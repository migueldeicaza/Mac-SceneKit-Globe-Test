
namespace SpriteKitEarth
open System
open MonoMac.SceneKit
open MonoMac.Foundation
open MonoMac.AppKit
open MonoMac.CoreAnimation
    
type GlobeScene() as this =
    inherit SCNScene()

    let cameraNode = 
        let camera = new SCNCamera (
                       ZNear = 0.01)
                       // FocalBlurRadius = 0)
        let node = new SCNNode (
                    Position = SCNVector3  (0.0f, 0.0f, 1.5f),
                    Camera = camera)
        node

    let ambientNode = 
        let ambientLight = new SCNLight (LightType = SCNLightType.Ambient.ToString (), Color = NSColor.FromCalibratedWhite (0.5f, 1.0f))
        let node = new SCNNode (
                    Light = ambientLight)
        node            

    let globeGeometry = 
        let geo = SCNSphere.Create 0.5f
        let material = geo.FirstMaterial
        material.Diffuse.Contents <- NSImage.ImageNamed ("earth_diffuse.jpg")
        material.Ambient.Contents <- NSImage.ImageNamed ("earth_ambient2.jpeg")
        material.Specular.Contents <- NSImage.ImageNamed ("earth_specular.jpg")
        material.Normal.Contents <- NSImage.ImageNamed ("earth_normals.png")
        geo

    let globeModel = new SCNNode (Geometry = globeGeometry)

    let cloudGeometry = 
        let geo = SCNSphere.Create 0.501f
        let material = geo.FirstMaterial
        material.Transparent.Contents <- NSImage.ImageNamed ("earth_clouds.png")
        geo

    let cloudModel = new SCNNode (Geometry = cloudGeometry)

    let animation = CABasicAnimation.FromKeyPath ("rotation", 
                        To = NSValue.FromVector (SCNVector4 (1.0f, 1.0f, 0.0f, float32 (Math.PI*2.0))), 
                        Duration = 5.0, RepeatCount = 10000.0f )

    let globeNode = 
        let node = new SCNNode ()
        node.AddChildNode globeModel
        node.AddChildNode cloudModel
        node.AddAnimation (animation, new NSString("Key"))
        node

    do 
        this.RootNode.AddChildNode cameraNode 
        this.RootNode.AddChildNode ambientNode
        this.RootNode.AddChildNode globeNode