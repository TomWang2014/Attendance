// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeList.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <summary>
//   TypeList，类型列表
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A shortcut for <see cref="TypeList{TBaseType}"/> to use object as base type.
    /// </summary>
    public class TypeList : TypeList<object>
    {
    }

    /// <summary>
    /// Extends <see cref="List{Type}"/> to add restriction a specific base type.
    /// </summary>
    /// <typeparam name="TBaseType">
    /// Base Type of <see cref="Type"/>s in this list
    /// </typeparam>
    public class TypeList<TBaseType> : ITypeList<TBaseType>
    {
        /// <summary>
        /// typeList
        /// </summary>
        private readonly List<Type> typeList;

        /// <summary>
        /// Creates a new <see cref="TypeList{T}"/> object.
        /// </summary>
        public TypeList()
        {
            this.typeList = new List<Type>();
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count
        {
            get
            {
                return this.typeList.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="Type"/> at the specified index.
        /// </summary>
        /// <param name="index">
        /// Index.
        /// </param>
        /// <returns>
        /// type
        /// </returns>
        public Type this[int index]
        {
            get
            {
                return this.typeList[index];
            }

            set
            {
                CheckType(value);
                this.typeList[index] = value;
            }
        }

        /// <inheritdoc/>
        public void Add<T>() where T : TBaseType
        {
            this.typeList.Add(typeof(T));
        }

        /// <inheritdoc/>
        public void Add(Type item)
        {
            CheckType(item);
            this.typeList.Add(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, Type item)
        {
            this.typeList.Insert(index, item);
        }

        /// <inheritdoc/>
        public int IndexOf(Type item)
        {
            return this.typeList.IndexOf(item);
        }

        /// <inheritdoc/>
        public bool Contains<T>() where T : TBaseType
        {
            return this.Contains(typeof(T));
        }

        /// <inheritdoc/>
        public bool Contains(Type item)
        {
            return this.typeList.Contains(item);
        }

        /// <inheritdoc/>
        public void Remove<T>() where T : TBaseType
        {
            this.typeList.Remove(typeof(T));
        }

        /// <inheritdoc/>
        public bool Remove(Type item)
        {
            return this.typeList.Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            this.typeList.RemoveAt(index);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            this.typeList.Clear();
        }

        /// <inheritdoc/>
        public void CopyTo(Type[] array, int arrayIndex)
        {
            this.typeList.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public IEnumerator<Type> GetEnumerator()
        {
            return this.typeList.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.typeList.GetEnumerator();
        }

        /// <inheritdoc/>
        private static void CheckType(Type item)
        {
            if (!typeof(TBaseType).IsAssignableFrom(item))
            {
                throw new ArgumentException("Given item is not type of " + typeof(TBaseType).AssemblyQualifiedName, "item");
            }
        }
    }
}