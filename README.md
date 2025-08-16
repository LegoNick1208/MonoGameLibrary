# MonoGameLibrary
Developed by Nick Shreve
## Requirements
This library depends on [MonoGame](https://monogame.net/) for rendering and [Facepunch Steamworks](https://wiki.facepunch.com/steamworks/) for connecting to Steam.\
Make sure you have installed both prior to developing with this library. I would reccomend starting projects with one of the MonoGame project templates they provide. See their documentation for information.
## Installation
You can download this source code and compile it from the source, or download lib.dll directly. Regardless, you will want to add a reference to this DLL in your project.\
If you compile from source, you will need to make sure you are compiling with support for C# `unsafe`. Some networking code uses pointers and pinning memory.\
You will also want to make sure Facepunch Steamworks is referenced, as well as the [Steamworks SDK](https://partner.steamgames.com/). As of now, Facepunch's NuGet package is dated to SDK v1.48a. Just make sure you are using the correct SDK version.\
Your output directory should contain all of MonoGame's stuff (handled by the project template if you used it), Facepunch Steamworks, this library, and the Steam API.\
I have only tested this with x64 based Steamworks. It should theoretically work for other architectures, but I have not confirmed.
## Usage
This assumes you know the basics of MonoGame
### The main class
Instead of extending MonoGame's `Game` class, extend either `Core` or `SteamCore` depending on if you want your game to be Steam-Enabled.\
I would reccomend making this primary class partial, so that you can split overrides by functionality. This is up to you.\
As a rule of thumb, all overrides should call their base before executing any code. A notable exception is `Draw`, however you should never need to override that anyways. If you don't know if you should call the base method, refer back to the source code here. If it does something, odds are you should call it.\
You can pass a `CoreConfig` to the base constructor if you wish to configure some aspects of your project. See it for details.
### Rendering
Rendering in this library is done by managing instances of rendering objects. After creating an object, the library will handle updating it every frame until deletion.\
**DO NOT LOSE HANDLES TO OBJECTS**. This library maintains it's own references, so they will not be garbage collected **AND** will still be rendered even if you let it go out of scope. If you no longer want an object, call `Delete` on that object before dropping it's scope.\
Currently the library supports 3 subclasses of `RenderObject`:
* `FilledRectangle`
* `Sprite`
* `Text`

These all do what you would expect. You can investigate each class for their individual properties.\
`Sprite`s **MUST** be created **AFTER** MonoGame has loaded your sprites into memory. This is done during the `base.LoadContent()` call.\
This libray uses a Texture Atlas system, similar to what the MonoGame Tutorial uses. I would reccomend reading up on that if you don't know how that works. The XML Structure is identical to the one used in those tutorials, so the example spritesheet + XML file it uses can be incorporated into this libray to test it out and use as an example. To load your textures into the atlas, call `Core.LoadAtlas` from `LoadContent` and pass it the local paths to all of your atlas xml files.\
Animated Sprites are currently in progress. They will be added eventually when I get around to finishing them.\

To summarize, the basics are:
1. Create instances of the rendering objects (Typically as member variables of some class)
2. Modify them as you wish during each `Update` cycle
3. `Delete` them when you are done with them

### Networking
As stated, this library has support for connecting and networking through Steam. Currently, it only supports P2P Networking\
You need to extend `SteamCore` for access to Steam. Once you do, you can call `HostServer(playerCount)` to start hosting.\
There are then many methods you can override for handling all sorts of events related to the server. Check `SteamCore` to see the full list. These will give you hooks to all sorts of networking information and events.\
Remember, you are writing both client **AND** host at the same time - make sure you keep everything straight. (Remember the partial classes suggestion from before? This is when that pays off)\
All messages are sent as `byte[]`s. You will need to come up with your own encoding and packet system, the hooks just handle the pointer manipulation and unsafe C# so you don't have to.\
Also make sure at least you override `OnCLientConnectionRequested` and call `connection.Accept()`. Otherwise, nobody can ever join! This is not done by default incase you want any special filtering code to reject some requests.\
By default, the AppID is 480 for Spacewar. If you ever publish a game, make sure to change this in the `SteamCoreConfig`!
## License
MIT License, enjoy!
