using System;
using System.Runtime.CompilerServices;

namespace UnderstandingDotNet.CustomTasks.Domain
{
    [AsyncMethodBuilder(typeof(TaskTraceMethodBuilder<>))]
    public sealed class TaskTrace<T> : INotifyCompletion
    {
        private T _result;

        private Action _continuation;

        public bool IsCompleted { get; private set; }

        public TaskTrace<T> GetAwaiter() => this;

        public void SetResult(T result)
        {
            _result = result;
            IsCompleted = true;
            _continuation?.Invoke();
        }

        public void OnCompleted(Action continuation)
        {
            _continuation = continuation;
            if (IsCompleted) continuation();
        }

        public T GetResult() => IsCompleted
            ? _result
            : throw new Exception("Not completed");

        public void SetException(Exception exception)
        {
            /* */
        }
    }
}