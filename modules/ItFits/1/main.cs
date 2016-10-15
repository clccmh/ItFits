//-----------------------------------------------------------------------------
// Copyright (c) 2013 GarageGames, LLC
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
//-----------------------------------------------------------------------------

function ItFits::create( %this )
{
    exec("./scripts/controls.cs");
    exec("./gui/guiProfiles.cs");

    // We need a main "Scene" we can use as our game world.  The place where sceneObjects play.
    // Give it a global name "mainScene" since we may want to access it directly in our scripts.
    new Scene(mainScene);

    // Without a system window or "Canvas", we can't see or interact with our scene.
    // AppCore initialized the Canvas already

    // Now that we have a Canvas, we need a viewport into the scene.
    // Give it a global name "mainWindow" since we may want to access it directly in our scripts.
    new SceneWindow(mainWindow);
    //mainWindow.profile = new GuiControlProfile();
    Canvas.setContent(mainWindow);

    // Finally, connect our scene into the viewport (or sceneWindow).
    // Note that a viewport comes with a camera built-in.
    mainWindow.setScene(mainScene);
    mainWindow.setCameraPosition( 0, 0 );
    mainWindow.setCameraSize( 100, 75 );

    // load some scripts and variables
    // exec("./scripts/someScript.cs");

    // let's do a little something to make sure we are up and running.
    // write "hello world!"  :)
    //%this.sayHello();

    %board = new Sprite();
    %board.Image = "ItFits:board";
    %board.position = "-25 0";
    %board.size = "50 50";
    %board.SceneLayer = 20;
    mainScene.add(%board);

    %line1 = %this.makeVertLine(2, "0 0");
    %line2 = %this.makeVertLine(1, "0 0");
    %line1.setSceneLayer(5);
    %line2.setSceneLayer(5);
    %line1.setBodyType("static");
    echo("collision shape: ", %line1.createPolygonBoxCollisionShape(5, 20));
    %line1.setCollisionLayers(5);
    %line1.setCollisionGroup(5);
    %line2.setBodyType("static");
    echo("collision shape: ", %line2.createPolygonBoxCollisionShape(5, 20));
    %line2.setCollisionLayers(5);
    %line2.setCollisionGroup(5);

    mainScene.add(%line1);
    mainScene.add(%line2);

    %line2.setPosition("10 0");

    new ScriptObject(InputManager);
    mainWindow.addInputListener(InputManager);
}

//-----------------------------------------------------------------------------

function ItFits::destroy( %this )
{
  InputManager.delete();
}

function ItFits::makeBlock(%this, %color, %pos)
{
    %block = new Sprite();
    %block.Image = "ItFits:blocks";
    %block.frame = %color;
    %block.position = %pos;
    %block.size = "5 5";
    %block.SceneLayer = 1;
    return %block;
}

function ItFits::makeVertLine(%this, %color, %pos)
{
    %line = new CompositeSprite();
    %line.DefaultSpriteStride = "5";
    %line.DefaultSpriteSize = "5";
    %line.addSprite(%pos);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x SPC %pos.y-5);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x SPC %pos.y-10);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x SPC %pos.y-15);
    %line.setSpriteImage("ItFits:blocks", %color);
    return %line;
}

//-----------------------------------------------------------------------------


function ItFits::sayHello( %this )
{
    %phrase = new ImageFont();
    %phrase.Image = "ItFits:font";

    // Set the font size in both axis.  This is in world-units and not typicaly font "points".
    %phrase.FontSize = "4 4";

    %phrase.TextAlignment = "Center";
    %phrase.Text = "It Fits!";
    %phrase.SceneLayer = 20;
    mainScene.add( %phrase );
}
