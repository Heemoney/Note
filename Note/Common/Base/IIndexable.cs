using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Common.Base
{
   
    /// <summary>
    /// Specifies an indexer.
    /// </summary>
    /// <typeparam name="TKey">The element type of the key</typeparam>
    /// <typeparam name="TVal">The element type of the value</typeparam>
    public interface IIndexable<TKey, TVal>
    {
        TVal this[TKey key] { get; set; }
    }

    /// <summary>
    /// Specifies an indexer.
    /// </summary>
    /// <typeparam name="TKey">The element type of the key</typeparam>
    /// <typeparam name="TVal">The element type of the value</typeparam>
    public interface IIndexableReadOnly<TKey, TVal>
    {
        TVal this[TKey key] { get; }
    }
    /// <summary>
    /// Specifies an indexer.
    /// </summary>
    /// <typeparam name="TKey">The element type of the key</typeparam>
    /// <typeparam name="TVal">The element type of the value</typeparam>
    public interface IIndexableDouble<TKey, TVal>
    {
        TVal this[TKey key, TKey key2] { get; set; }
    }

    /// <summary>
    /// Specifies an indexer.
    /// </summary>
    /// <typeparam name="TKey">The element type of the key</typeparam>
    /// <typeparam name="TVal">The element type of the value</typeparam>
    public interface IIndexableDoubleReadOnly<TKey, TVal>
    {
        TVal this[TKey key, TKey key2] { get; }
    }
}
