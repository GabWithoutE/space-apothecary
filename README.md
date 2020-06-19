# space-apothecary

This is an incomplete game that was developed by me and a friend who does some art in a month long game jam. I still need to come in and clean up the code, and implement/refactor certain things to get it to conform to some patterns that I've been considering. It's been a side project. In this project, I explore design ideas using scriptable objects as a way to share data between components--the goal of which is to decouple components, and increase flexibility, increasing and taking advantage of Unity Editor's convenience.

## Experiments to Note
### Problems
1. Reusability -- It would be greate if developers could drop scripts onto objects and add functionality easily. A problem that needs to be addressed, though, is the shared state between different scripts. Prefab's seem like a really great way to package plug and play behaviour, but due to script dependencies and coupling which is common in the Unity pattern of using "GetComponent" or injecting GameObjects as arguments in the editor, the way to use prefabs to plug and play functionality is not obvious.
2. Package -- How to make the code reusable between projects. 

### Solutions
#### Reusability 
Solution Description: 
- State... Using ScriptableObjects to wrap variables keeps state separate from GameObjects or Prefabs. These states can be injected using Unity Editor's serializable fields, because ScriptableObjects are Serializable. The Interface for these state variables includes the ability to write to the variables, read. 
- Behaviour Delegation... ScriptableObjects are... scriptable. An issue that comes with only using Monobehaviours is that the interdependencies between components are always made using "GetComponent" which hides dependencies from the developer in the Scene view. (Note: Monobehaviours can use comments to "require" other Monobehaviours, so the Unity Editor will warn developers of these dependencies). In order for the ScriptableObjects to be attached to certain GameObjects/Entities in a scene, Reusable Monobehaviours can be used as a way to "hook" into the Unity update loop. Something to note is that this behaviour can be decoupled/made reusable, by not having state in the "BehaviourObjects". Having entity specific state passed in using the Object's API allows 1 of these objects (instances) to be used for multiple entities.

Drawbacks:
- ScriptableObjects are kind of like instantiated classes--from an object oriented perspective. It's kind of weird that these objects that are conceptually "runtime" live in the filestructure with "classes/scripts". It's an important part of the programming pattern to organize the filestructure in a particular way. This is one area I'm going to work on refactoring. 
- Interfaces aren't Serializable without extra UnityEditor work. Therefore, it isn't trivial to create injectable fields in the Editor of type Interface, in order to encapsulate certain behaviour. The workaround for this is to make other ScriptableObjects that wrap around existing ones, that only have certain functionality. 

Learnings/Continued work:
- Editor work to enable Interface "Serializability"
- Better File Structure
- More consistent pattern across the project.

## Next Steps
Clean up and continue to explore design patterns in Unity. Clean up some of the "out-of-pattern" features. Pull out reusable code to make it accessible as boilerplate for future projects.
