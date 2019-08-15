#include "Enumerable.h"
#include <thread>
#include <math.h>       /* floorl & ln (log)*/
#include <stdlib.h>
#include <mutex>

constexpr auto THREAD_CAP = 50;
constexpr auto THREAD_MIN = 1;

using std::thread;
using std::mutex;

mutex mu;

void copy_internal_partition(long* src, long* partitioned_dest, unsigned partition_rw, unsigned offset);
void internal_thread_forker(long* src, long* dst, unsigned partition, unsigned offset);

void __stdcall hyper_iterator_cpy_UNMANAGED(void* src, int src_idx, void* dst, int dest_idx, int len) {
	/*Casting to long* to do pointer arithmatic later*/
	long* l_src = (long*)src;
	long* l_dst = (long*)dst;

	short num_threads = (short) log(len) * 2; //natural log 

	if (num_threads == 0) {
		num_threads = THREAD_MIN;
	}

	if (num_threads > THREAD_CAP) {
		num_threads = THREAD_CAP;
	}

	double thread_appx = len / num_threads;
	unsigned thread_def_partition = (unsigned) floorl(thread_appx);

	bool obtuse_partition = len % 2 == 0 && (len / thread_def_partition) % 2 != 0;

	thread *t_harness = new thread[num_threads];
	unsigned offset = 0;
	for (int i = 0; i < num_threads; i++) {

		if (i == num_threads - 1 && obtuse_partition) {
			thread_def_partition = len - offset;
		}

		long* iso_src = new long[thread_def_partition];
		copy_internal_partition(l_src, iso_src, thread_def_partition, offset);
		
		t_harness[i] = thread(std::ref(internal_thread_forker), iso_src, l_dst, thread_def_partition, offset);
		offset += thread_def_partition;
	}

	for (int i = 0; i < num_threads; i++) {
		t_harness[i].join();
	}

	delete[] t_harness;
}

void copy_internal_partition(long* src, long* partitioned_dst, unsigned partition_rw, unsigned offset) {
	unsigned partition_as_bytes = partition_rw * sizeof(*src);
	memcpy(partitioned_dst, src + offset, partition_as_bytes);
}

void internal_thread_forker(long *src, long *dst, unsigned partition_rw, unsigned offset) {
	mu.lock();
	unsigned partition_as_bytes = partition_rw * sizeof(*src); //double check
	memcpy(dst + offset, src, partition_as_bytes);
	mu.unlock();
}

