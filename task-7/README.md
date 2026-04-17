# Asynchronous Programming and Multi-threading

- Develop a console application that performs multiple asynchronous operations concurrently.
- Use `async` and `await` to fetch data from multiple simulated sources (e.g., using `Task.Delay` to mimic API calls).
- Aggregate the results once all tasks are complete.
- Handle exceptions that may occur during asynchronous operations

## Output

```txt
Starting async operations...

Source 1 fetching started...
Source 2 fetching started...
Source 3 fetching started...
Source 3 completed.
Source 1 completed.
Source 2 completed.

All tasks completed. Aggregated Results:

Source 1 data retrieved successfully.
Source 2 data retrieved successfully.
Source 3 data retrieved successfully.

Press any key to exit...
e
```
