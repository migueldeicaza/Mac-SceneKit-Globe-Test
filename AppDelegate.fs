
namespace SpriteKitEarth

open System
open System.Drawing

open MonoMac.ObjCRuntime
open MonoMac.Foundation
open MonoMac.AppKit

[<Register ("AppDelegate")>]
type AppDelegate () =
    inherit NSApplicationDelegate ()

    [<DefaultValue>]
    val mutable mainWindowController : SpriteKitEarth.MainWindowController

    override x.FinishedLaunching (notification) =
        let mainWindowController = new SpriteKitEarth.MainWindowController ()
        mainWindowController.Window.MakeKeyAndOrderFront (x)

module main =
    [<EntryPoint>]
    let main args =
        NSApplication.Init ()
        NSApplication.Main (args)
        0

