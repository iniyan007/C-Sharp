# Delegates, Events, and Basic Event Handling

- Build a console-based event-driven application (e.g., a counter that triggers an event at a threshold).
- Define a delegate and an event that fires when a counter reaches a specific value.
- Create multiple event handler methods that perform actions when the event is raised.
- In your main loop, increment the counter and raise the event when appropriate.
- Demonstrate how events can decouple the producer and consumer logic.

## 🧠 How This Demonstrates Decoupling

- The Counter class (producer):
    - Does NOT know what happens when threshold is reached
    - Just raises an event
- The Handlers (consumers):
    - Decide what to do (alert, log, suggest reset)
    - Can be added/removed without modifying Counter
- 👉 This is loose coupling, which is a key principle in scalable systems.

## Output

```txt
Counter Value: 1
Counter Value: 2
Counter Value: 3
Counter Value: 4
Counter Value: 5
Alert! Threshold reached at 5
Logging: Counter hit 5
Suggestion: Consider resetting the counter.
Counter Value: 6
Counter Value: 7
Counter Value: 8
Counter Value: 9
Counter Value: 10
Program finished.
```
