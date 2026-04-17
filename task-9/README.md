# Reflection and Custom Attributes

- Build an application that discovers and executes methods based on custom attributes.
- Define a custom attribute (e.g., `[Runnable]`).
- Create several classes with methods decorated with the `[Runnable]` attribute.
- Use reflection to scan the current assembly for methods marked with `[Runnable]`.
- Invoke the discovered methods dynamically and display their outputs.

## Output

```txt
Starting Runnable App...

Running: TaskOne.RunTask
TaskOne executed
Running: TaskTwo.Execute
TaskTwo executed

Done.
```
