# How to use Visual Debug

To use VisualDebug, you just have to add the 'Visual Debug' prefab located in the resources folder to your main scene. It will not be destroyed between scenes, so you only need to add it once.

You do not need to change anything in your code to use VisualDebug. It will capture your Logger and Debug log calls, exceptions... Every log message that your application receives. 

## Customizing

To customize VisualDebug, modify the VisualDebug prefab and LogColors, both are located in the Resources folder.  

Modifying the Visual Debug prefab you can change:  
* By editing its Visual Debug script:  
    * The Debug window opacity when visible  
    * Start visible or not  
    * Visibility toggle key (Tab by default)  
    * Clean key (Delete by default)  
* Its Unity UI:  
    * Its background image, in the child called "Image"  
    * Its font configuration, in the child called "Text"  
    * Its scrollbar, in the child called "Scrollbar"  

Modifying the LogColors scriptable object, you can change the color associated with each message type. These are: Log, Warning, Exception, Error and Assert.