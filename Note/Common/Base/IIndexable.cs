using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Common.Base
{
   
    /// <summary>
    /// Specifies an indexer with one dimension.
    /// </summary>
    /// <typeparam name="TKey">The element type of the key</typeparam>
    /// <typeparam name="TVal">The element type of the value</typeparam>
    public interface IIndexable<TKey, TVal>
    {
        TVal this[TKey key] { get; set; }
    }

    /// <summary>
    /// Specifies an indexer with one dimension read-only.
    /// </summary>
    /// <typeparam name="TKey">The element type of the key</typeparam>
    /// <typeparam name="TVal">The element type of the value</typeparam>
    public interface IIndexableReadOnly<TKey, TVal>
    {
        TVal this[TKey key] { get; }
    }
    /// <summary>
    /// Specifies an indexer with two dimensions.
    /// </summary>
    /// <typeparam name="TKey">The element type of the key</typeparam>
    /// <typeparam name="TVal">The element type of the value</typeparam>
    public interface IIndexableDouble<TKey, TVal>
    {
        TVal this[TKey key, TKey key2] { get; set; }
    }

    /// <summary>
    /// Specifies an indexer with two dimensions read-only.
    /// </summary>
    /// <typeparam name="TKey">The element type of the key</typeparam>
    /// <typeparam name="TVal">The element type of the value</typeparam>
    public interface IIndexableDoubleReadOnly<TKey, TVal>
    {
        TVal this[TKey key, TKey key2] { get; }
    }

    /// <summary>
    /// Specifies an indexer with three dimensions.
    /// </summary>
    /// <typeparam name="TKey">The element type of the key</typeparam>
    /// <typeparam name="TVal">The element type of the value</typeparam>
    public interface IIndexableTriple<TKey, TVal>
    {
        TVal this[TKey key, TKey key2, TKey key3] { get; set; }
    }

    /// <summary>
    /// Specifies an indexer with three dimensions read-only.
    /// </summary>
    /// <typeparam name="TKey">The element type of the key</typeparam>
    /// <typeparam name="TVal">The element type of the value</typeparam>
    public interface IIndexableTripleReadOnly<TKey, TVal>
    {
        TVal this[TKey key, TKey key2, TKey key3] { get; }
    }
}
