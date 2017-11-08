using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class LazyLoadedList<T> : ICollection<T>, IEnumerable<T>, IList<T>,IReadOnlyCollection<T>, IReadOnlyList<T>, IList {
    private IMapper mapper;
    private DbContext context;

    public LazyLoadedList(IMapper mapper, DbContext context) {
        this.mapper = mapper;
        this.context = context;
    }

    public T this[int index] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    object IList.this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public int Count => throw new System.NotImplementedException();

    public bool IsReadOnly => throw new System.NotImplementedException();

    public bool IsFixedSize => throw new NotImplementedException();

    public bool IsSynchronized => throw new NotImplementedException();

    public object SyncRoot => throw new NotImplementedException();

    public void Add(T item) {

    }

    public int Add(object value)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new System.NotImplementedException();
    }

    public bool Contains(T item)
    {
        throw new System.NotImplementedException();
    }

    public bool Contains(object value)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new System.NotImplementedException();
    }

    public void CopyTo(Array array, int index)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    public int IndexOf(T item)
    {
        throw new System.NotImplementedException();
    }

    public int IndexOf(object value)
    {
        throw new NotImplementedException();
    }

    public void Insert(int index, T item)
    {
        throw new System.NotImplementedException();
    }

    public void Insert(int index, object value)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T item)
    {
        throw new System.NotImplementedException();
    }

    public void Remove(object value)
    {
        throw new NotImplementedException();
    }

    public void RemoveAt(int index)
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new System.NotImplementedException();
    }
}