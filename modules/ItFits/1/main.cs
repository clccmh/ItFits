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
    $overlap = %this.overlapMessage();
    mainScene.add($overlap);
    $overlap.setVisible(false);

    $notOnBoard = %this.notOnBoardMessage();
    mainScene.add($notOnBoard);
    $notOnBoard.setVisible(false);

    %board = new Sprite();
    %board.Image = "ItFits:board";
    %board.position = "-25 0";
    %board.size = "50 50";
    %board.SceneLayer = 20;
    mainScene.add(%board);

    %this.level1();

    new ScriptObject(InputManager);
    mainWindow.addInputListener(InputManager);
}

//-----------------------------------------------------------------------------

function ItFits::destroy( %this )
{
  InputManager.delete();
}

function ItFits::level1(%this)
{
  echo("Started Level 1");

  %shapes[0] = %this.makeVertLine(getRandom(0, 4), "0 0");
  %shapes[1] = %this.makeHorizLine(getRandom(0, 4), "0 0");
  //%shapes[2] = %this.makeLeftLHoriz(getRandom(0, 4), "0 0");
  //%shapes[3] = %this.makeZVert(getRandom(0, 4), "0 0");
  //%shapes[4] = %this.makeLeftLHorizInverse(getRandom(0, 4), "0 0");
  //%shapes[5] = %this.makeZHoriz(getRandom(0, 4), "0 0");

  for (%i = 0; %i < 2; %i++)
  {
    %shapes[%i].setSceneLayer(5);
    mainScene.add(%shapes[%i]);
    %shapes[%i].setPosition("20 0");
  }

  //%line1.setBodyType("static");
  //%line1.createPolygonBoxCollisionShape(5, 20);
  //%line1.setCollisionLayers(5);
  //%line2.setBodyType("static");
  //%line2.createPolygonBoxCollisionShape(5, 20);
  //%line2.setCollisionLayers(5);
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
    %line.DefaultSpriteStride = "1";
    %line.DefaultSpriteSize = "5";
    %line.addSprite(%pos);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x SPC %pos.y-5);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x SPC %pos.y-10);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x SPC %pos.y-15);
    %line.setSpriteImage("ItFits:blocks", %color);
    echo("vert line size: ", %line.getSize());
    echo("vert line pos: ", %line.getPosition());
    return %line;
}

function ItFits::makeHorizLine(%this, %color, %pos)
{
    %line = new CompositeSprite();
    %line.DefaultSpriteStride = "5";
    %line.DefaultSpriteSize = "5";
    %line.addSprite(%pos);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-5 SPC %pos.y);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-10 SPC %pos.y);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-15 SPC %pos.y);
    %line.setSpriteImage("ItFits:blocks", %color);
    return %line;
}

function ItFits::makeLeftLVert(%this, %color, %pos)
{
    %line = new CompositeSprite();
    %line.DefaultSpriteStride = "5";
    %line.DefaultSpriteSize = "5";
    line.addSprite(%pos);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x SPC %pos.y-5);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x SPC %pos.y-10);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-5 SPC %pos.y-10);
    %line.setSpriteImage("ItFits:blocks", %color);
    return %line;
}

function ItFits::makeRightLVert(%this, %color, %pos)
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
    %line.addSprite(%pos.x+5 SPC %pos.y-10);
    %line.setSpriteImage("ItFits:blocks", %color);
    return %line;
}

function ItFits::makeRightLHoriz(%this, %color, %pos)
{
    %line = new CompositeSprite();
    %line.DefaultSpriteStride = "5";
    %line.DefaultSpriteSize = "5";
    %line.addSprite(%pos);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-5 SPC %pos.y);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-10 SPC %pos.y);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-10 SPC %pos.y-5);
    %line.setSpriteImage("ItFits:blocks", %color);
    return %line;
}

function ItFits::makeLeftLHoriz(%this, %color, %pos)
{
    %line = new CompositeSprite();
    %line.DefaultSpriteStride = "5";
    %line.DefaultSpriteSize = "5";
    %line.addSprite(%pos);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-5 SPC %pos.y);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-10 SPC %pos.y);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-10 SPC %pos.y+5);
    %line.setSpriteImage("ItFits:blocks", %color);
    return %line;
}

function ItFits::makeRightLHorizInverse(%this, %color, %pos)
{
    %line = new CompositeSprite();
    %line.DefaultSpriteStride = "5";
    %line.DefaultSpriteSize = "5";
    %line.addSprite(%pos);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x SPC %pos.y+5);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-5 SPC %pos.y);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-10 SPC %pos.y);
    %line.setSpriteImage("ItFits:blocks", %color);
    return %line;
}

function ItFits::makeLeftLHorizInverse(%this, %color, %pos)
{
    %line = new CompositeSprite();
    %line.DefaultSpriteStride = "5";
    %line.DefaultSpriteSize = "5";
    %line.addSprite(%pos);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x SPC %pos.y-5);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-5 SPC %pos.y);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-10 SPC %pos.y);
    %line.setSpriteImage("ItFits:blocks", %color);
    return %line;
}

function ItFits::makeZVert(%this, %color, %pos)
{
    %line = new CompositeSprite();
    %line.DefaultSpriteStride = "5";
    %line.DefaultSpriteSize = "5";
    %line.addSprite(%pos);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x SPC %pos.y-5);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x+5 SPC %pos.y-5);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x+5 SPC %pos.y-10);
    %line.setSpriteImage("ItFits:blocks", %color);
    return %line;
}

function ItFits::makeZVertInverse(%this, %color, %pos)
{
    %line = new CompositeSprite();
    %line.DefaultSpriteStride = "5";
    %line.DefaultSpriteSize = "5";
    %line.addSprite(%pos);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x SPC %pos.y-5);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-5 SPC %pos.y-5);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-5 SPC %pos.y-10);
    %line.setSpriteImage("ItFits:blocks", %color);
    return %line;
}

function ItFits::makeZHoriz(%this, %color, %pos)
{
    %line = new CompositeSprite();
    %line.DefaultSpriteStride = "5";
    %line.DefaultSpriteSize = "5";
    %line.addSprite(%pos);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-5 SPC %pos.y);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-5 SPC %pos.y-5);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-10 SPC %pos.y-5);
    %line.setSpriteImage("ItFits:blocks", %color);
    return %line;
}

function ItFits::makeZHorizInverse(%this, %color, %pos)
{
    %line = new CompositeSprite();
    %line.DefaultSpriteStride = "5";
    %line.DefaultSpriteSize = "5";
    %line.addSprite(%pos);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-5 SPC %pos.y);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-5 SPC %pos.y+5);
    %line.setSpriteImage("ItFits:blocks", %color);
    %line.addSprite(%pos.x-10 SPC %pos.y+5);
    %line.setSpriteImage("ItFits:blocks", %color);
    return %line;
}


//-----------------------------------------------------------------------------


function ItFits::overlapMessage( %this )
{
    %phrase = new ImageFont();
    %phrase.Image = "ItFits:font";

    // Set the font size in both axis.  This is in world-units and not typicaly font "points".
    %phrase.FontSize = "2 3";

    %phrase.TextAlignment = "Center";

    %phrase.Text = "Blocks cannot overlap. Try again!";
    %phrase.SceneLayer = 0;
    %phrase.setPosition(0,35);
    return %phrase;
}

function ItFits::notOnBoardMessage( %this )
{
    %phrase = new ImageFont();
    %phrase.Image = "ItFits:font";

    // Set the font size in both axis.  This is in world-units and not typicaly font "points".
    %phrase.FontSize = "2 3";

    %phrase.TextAlignment = "Center";

    %phrase.Text = "Block must be placed on the board. Try again!";
    %phrase.SceneLayer = 0;
    %phrase.setPosition(0,35);
    return %phrase;
}
