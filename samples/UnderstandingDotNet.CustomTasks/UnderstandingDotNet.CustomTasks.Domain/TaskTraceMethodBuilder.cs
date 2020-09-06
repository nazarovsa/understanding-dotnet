using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnderstandingDotNet.CustomTasks.Domain
{
    public struct TaskTraceMethodBuilder<T>
    {
        private readonly Stopwatch _stopwatch;

        public TaskTraceMethodBuilder(Stopwatch stopwatch)
        {
            _stopwatch = stopwatch;
            Task = new TaskTrace<T>();
        }

        public TaskTrace<T> Task { get; }

        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            Console.WriteLine("Start executing task...");
            _stopwatch.Start();
            stateMachine.MoveNext();
        }

        public void SetException(Exception exception)
        {
            Task.SetException(exception);
        }

        public void SetResult(T result)
        {
            _stopwatch.Stop();
            Console.WriteLine($"Task executed... Elapsed {_stopwatch.ElapsedMilliseconds} ms");
            Task.SetResult(result);
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine =>
            awaiter.OnCompleted(stateMachine.MoveNext);

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter,
            ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine =>
            awaiter.OnCompleted(stateMachine.MoveNext);

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            /* */
        }

        public static TaskTraceMethodBuilder<T> Create()
        {
            return new TaskTraceMethodBuilder<T>(new Stopwatch());
        }
    }
}