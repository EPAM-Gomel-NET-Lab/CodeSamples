using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TPL
{
    /// <summary>
    /// Shows usage of most common synchronisation constructs
    /// </summary>
    public class Syncronization : IAsyncCodeSample
    {
        private const int numberOfThreads = 500;
        private readonly object _lockObject = new();
        private int _counterLocked = 0;
        private int _counterForSemaphore = 0;
        private int _counterForAsyncLock = 0;
        private int _counterUnlocked = 0;
        private int _counterForInterlock = 0;
        private int _counterForMutex = 0;
        private readonly SemaphoreSlim _semaphoreForAsync = new SemaphoreSlim(1, 1); // this gives you same result as a lock statement
        private readonly Semaphore _semaphore = new Semaphore(4, 100);
        private readonly Mutex _mutex = new Mutex(false, "Demo_Mutex");

        /// <inheritdoc cref="IAsyncCodeSample"/>
        public async Task Run()
        {
            ////var tasksLocked = GetTasks(LockedSection);
            var tasksSemaphore = GetTasks(LockedSemaphoreSection);
            ////var tasksUnlocked = GetTasks(UnlockedSection);
            ////var tasksWithInterlock = GetTasks(x => InterlockedSection());
            ////var tasksWithAsyncLock = GetTasks(LockedAsyncSection);
            ////var tasksWithMutex = GetTasks(MutexSection); // this will throw exception at runtime!!!! Try to avoid using mutex methods in async context
            await Task.WhenAll(tasksSemaphore); // this is the way to execute a lot of async methods in parallel
            _mutex.Dispose();
        }

        private void UnlockedSection(int number)
        {
            _counterUnlocked += 10;
            Console.WriteLine($"Unlocked number iteration {number} value {_counterUnlocked}");
        }

        /// <summary>
        /// Essentially the same as LockedSection only without console output;
        /// </summary>
        private void InterlockedSection()
        {
            Interlocked.Add(ref _counterForInterlock, 25);
        }

        private void Semaphore(int number)
        {
            lock (_lockObject)
            {
                _counterLocked += 25;
                Console.WriteLine($"Locked number iteration {number} value {_counterLocked}");
            }
        }

        private void LockedSection(int number)
        {
            lock (_lockObject)
            {
                _counterLocked += 25;
                Console.WriteLine($"Locked number iteration {number} value {_counterLocked}");
            }
        }

        private void LockedSemaphoreSection(int number)
        {
            _semaphore.WaitOne();
            try
            {
                _counterForSemaphore += 10;
                Console.WriteLine($"Async lock number iteration {number} value {_counterForSemaphore}");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task LockedAsyncSection(int number)
        {
            await _semaphoreForAsync.WaitAsync();
            try
            {
                await Task.Yield();
                _counterForAsyncLock += 10;
                Console.WriteLine($"Async lock number iteration {number} value {_counterForAsyncLock}");
            }
            finally
            {
                _semaphoreForAsync.Release();
            }
        }

        private async Task MutexSection(int number)
        {
            _mutex.WaitOne();
            try
            {
                await Task.Yield();
                _counterForMutex += 10;
                Console.WriteLine($"Mutex number iteration {number} value {_counterForMutex}");
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        /// <inheritdoc cref="IAsyncCodeSample"/>
        public string SampleDescription => "This shows usage of lock, Semaphore, Mutex, Interlocked";

        private static IEnumerable<Task> GetTasks(Action<int> actionToExecute) =>
            Enumerable.Range(0, numberOfThreads).Select(x => Task.Run(() => actionToExecute(x)))
                .ToList();

        private static IEnumerable<Task> GetTasks(Func<int, Task> actionToExecute) =>
            Enumerable.Range(0, numberOfThreads).Select(actionToExecute)
                .ToList();
    }
}
