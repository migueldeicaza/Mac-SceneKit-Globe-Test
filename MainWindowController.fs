
namespace SpriteKitEarth

open System
open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit
open MonoMac.SceneKit

[<Register ("MainWindow")>]
type MainWindow =
    inherit NSWindow

    new (handle : IntPtr) = { inherit NSWindow (handle) }

    override this.AwakeFromNib () =
        base.AwakeFromNib ()

        let scene = new GlobeScene()

        let lightNode = 
            let light = new SCNLight (LightType = SCNLightType.Omni.ToString())
            let node = new SCNNode (Light = light, Position = SCNVector3 (0.0f, 10.0f, 10.0f))
            node
        
        scene.RootNode.AddChildNode lightNode
        let sceneView = new SCNView (this.ContentView.Bounds, 
                            Scene=scene, 
                            AllowsCameraControl = true,
                            BackgroundColor=NSColor.Black)
        this.ContentView.AddSubview sceneView

[<Register("MainWindowController")>]
type MainWindowController =
    inherit NSWindowController

    new () = { inherit NSWindowController ("MainWindow") }

    new (handle : IntPtr) = { inherit NSWindowController (handle) }

