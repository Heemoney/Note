#ifndef ENUMERABLE_H
#define ENUMERABLE_H


extern "C" {
	__declspec(dllexport) void __stdcall hyper_iterator_cpy_UNMANAGED(void *src, int src_idx, void *dst, int dest_idx, int len);
}

#endif

