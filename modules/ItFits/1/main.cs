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

    %startingMessage = %this.startingMessage();
    mainscene.add(%startingMessage);
    %this.schedule(2000, "setup");
    %this.schedule(2000, "pileTutorial");
    %this.schedule(4000, "boardTutorial");
    %this.schedule(6000, "lifeLineTutorial");
    %this.schedule(14000, "level1Message");

}

function ItFits::setup(%this)
{
  $overlap = %this.overlapMessage();
  mainScene.add($overlap);
  $overlap.setVisible(false);

  $notOnBoard = %this.notOnBoardMessage();
  mainScene.add($notOnBoard);
  $notOnBoard.setVisible(false);

  $board = new Sprite();
  $board.Image = "ItFits:board";
  $board.position = "-25 0";
  $board.size = "50 50";
  $board.SceneLayer = 20;
  mainScene.add($board);

  $delete = new Sprite();
  $delete.Image = "ItFits:delete";
  $delete.frame = 1;
  $delete.position = "42.5 35";
  $delete.size = "5 5";
  $delete.SceneLayer = 2;
  mainScene.add($delete);
  $delete.setVisible(false);

  $skip = new Sprite();
  $skip.Image = "ItFits:skip";
  $skip.frame = 1;
  $skip.position = "47.5 35";
  $skip.size = "5 5";
  $skip.SceneLayer = 2;
  mainScene.add($skip);
  $skip.setVisible(false);

  $expand = new Sprite();
  $expand.Image = "ItFits:expand";
  $expand.frame = 1;
  $expand.position = "37.5 35";
  $expand.size = "5 5";
  $expand.SceneLayer = 2;
  mainScene.add($expand);
  $expand.setVisible(false);

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
  $level = 1;

  $shapes[0] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[1] = %this.makeHorizLine(getRandom(0, 4), "0 0");
  $shapes[2] = %this.makeLeftLHoriz(getRandom(0, 4), "0 0");
  $shapes[3] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[4] = %this.makeLeftLHorizInverse(getRandom(0, 4), "0 0");
  $shapes[5] = %this.makeZHoriz(getRandom(0, 4), "0 0");
  $shapes[6] = %this.makeZHoriz(getRandom(0, 4), "0 0");
  $shapes[7] = %this.makeZHoriz(getRandom(0, 4), "0 0");
  $shapes[8] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[9] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[10] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[11] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[12] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[13] = null;
  $shapes[14] = null;
  $shapes[15] = null;

  echo("Shapes count: ", $shapes.count);
  for (%i = 0; %i < 13; %i++)
  {
    $shapes[%i].setSceneLayer(5);
    mainScene.add($shapes[%i]);
    $shapes[%i].setPosition("20 0");
  }
}

function ItFits::level1Message( %this )
{
  $board.setVisible(true);
  $skip.setVisible(true);
  $delete.setVisible(true);
  $expand.setVisible(true);
  for (%i = 0; %i < 13; %i++)
  {
    $shapes[%i].setVisible(true);
  }
  %phrase = new ImageFont();
  %phrase.Image = "ItFits:font";
  %phrase.FontSize = "2 3";
  %phrase.TextAlignment = "Center";
  %phrase.Text = "Let's start with an easy one!";
  %phrase.SceneLayer = 0;
  %phrase.setPosition(0,35);
  %phrase.setLifetime(2);
  mainScene.add(%phrase);
  $startTime = getRealTime();
}

function ItFits::level2(%this)
{
  echo("Started Level 2");
  $level = 2;
  $board.setVisible(true);

  $shapes[0] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[1] = %this.makeHorizLine(getRandom(0, 4), "0 0");
  $shapes[2] = %this.makeLeftLHoriz(getRandom(0, 4), "0 0");
  $shapes[3] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[4] = %this.makeLeftLHorizInverse(getRandom(0, 4), "0 0");
  $shapes[5] = %this.makeZHoriz(getRandom(0, 4), "0 0");
  $shapes[6] = %this.makeZHoriz(getRandom(0, 4), "0 0");
  $shapes[7] = %this.makeHorizLine(getRandom(0, 4), "0 0");
  $shapes[8] = %this.makeLeftLVert(getRandom(0, 4), "0 0");
  $shapes[9] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[10] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[11] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[12] = %this.makeZHorizInverse(getRandom(0, 4), "0 0");
  $shapes[13] = %this.makeRightLVert(getRandom(0, 4), "0 0");
  $shapes[14] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[15] = %this.makeLeftLHorizInverse(getRandom(0, 4), "0 0");

  echo("Shapes count: ", $shapes.count);
  for (%i = 0; %i < 15; %i++)
  {
    $shapes[%i].setSceneLayer(5);
    mainScene.add($shapes[%i]);
    $shapes[%i].setPosition("20 0");
  }

  %phrase = new ImageFont();
  %phrase.Image = "ItFits:font";
  %phrase.FontSize = "2 3";
  %phrase.TextAlignment = "Center";
  %phrase.Text = "This level will require some thought.";
  %phrase.SceneLayer = 0;
  %phrase.setPosition(0,35);
  %phrase.setLifetime(2);
  mainScene.add(%phrase);
}

function ItFits::level3(%this)
{
  echo("Started Level 3");
  $level = 3;
  $board.setVisible(true);

  $shapes[0] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[1] = %this.makeHorizLine(getRandom(0, 4), "0 0");
  $shapes[2] = %this.makeLeftLHoriz(getRandom(0, 4), "0 0");
  $shapes[3] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[4] = %this.makeLeftLHorizInverse(getRandom(0, 4), "0 0");
  $shapes[5] = %this.makeZHoriz(getRandom(0, 4), "0 0");
  $shapes[6] = %this.makeZHoriz(getRandom(0, 4), "0 0");
  $shapes[7] = %this.makeHorizLine(getRandom(0, 4), "0 0");
  $shapes[8] = %this.makeLeftLVert(getRandom(0, 4), "0 0");
  $shapes[9] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[10] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[11] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[12] = %this.makeZHorizInverse(getRandom(0, 4), "0 0");
  $shapes[13] = %this.makeRightLVert(getRandom(0, 4), "0 0");
  $shapes[14] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[15] = %this.makeLeftLHorizInverse(getRandom(0, 4), "0 0");
  $shapes[16] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[17] = %this.makeZHorizInverse(getRandom(0, 4), "0 0");
  $shapes[18] = %this.makeRightLHoriz(getRandom(0, 4), "0 0");
  $shapes[19] = %this.makeHorizLine(getRandom(0, 4), "0 0");
  $shapes[20] = %this.makeRightLVert(getRandom(0, 4), "0 0");

  echo("Shapes count: ", $shapes.count);
  for (%i = 0; %i < 20; %i++)
  {
    $shapes[%i].setSceneLayer(5);
    mainScene.add($shapes[%i]);
    $shapes[%i].setPosition("20 0");
  }

  %phrase = new ImageFont();
  %phrase.Image = "ItFits:font";
  %phrase.FontSize = "2 3";
  %phrase.TextAlignment = "Center";
  %phrase.Text = "This one is even harder.";
  %phrase.SceneLayer = 0;
  %phrase.setPosition(0,35);
  %phrase.setLifetime(2);
  mainScene.add(%phrase);
}

function ItFits::level4(%this)
{
  echo("Started Level 4");
  $level = 4;
  $board.setVisible(true);

  $shapes[0] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[1] = %this.makeHorizLine(getRandom(0, 4), "0 0");
  $shapes[2] = %this.makeLeftLHoriz(getRandom(0, 4), "0 0");
  $shapes[3] = %this.makeRightLVert(getRandom(0, 4), "0 0");
  $shapes[4] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[5] = %this.makeLeftLVert(getRandom(0, 4), "0 0");
  $shapes[6] = %this.makeZHoriz(getRandom(0, 4), "0 0");
  $shapes[7] = %this.makeHorizLine(getRandom(0, 4), "0 0");
  $shapes[8] = %this.makeLeftLVert(getRandom(0, 4), "0 0");
  $shapes[9] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[10] = %this.makeRightLVert(getRandom(0, 4), "0 0");
  $shapes[11] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[12] = %this.makeZHorizInverse(getRandom(0, 4), "0 0");
  $shapes[13] = %this.makeRightLVert(getRandom(0, 4), "0 0");
  $shapes[14] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[15] = %this.makeLeftLHorizInverse(getRandom(0, 4), "0 0");
  $shapes[16] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[17] = %this.makeZHorizInverse(getRandom(0, 4), "0 0");
  $shapes[18] = %this.makeRightLHoriz(getRandom(0, 4), "0 0");
  $shapes[19] = %this.makeHorizLine(getRandom(0, 4), "0 0");
  $shapes[20] = %this.makeRightLVert(getRandom(0, 4), "0 0");

  echo("Shapes count: ", $shapes.count);
  for (%i = 0; %i < 20; %i++)
  {
    $shapes[%i].setSceneLayer(5);
    mainScene.add($shapes[%i]);
    $shapes[%i].setPosition("20 0");
  }

  %phrase = new ImageFont();
  %phrase.Image = "ItFits:font";
  %phrase.FontSize = "2 3";
  %phrase.TextAlignment = "Center";
  %phrase.Text = "Level 4";
  %phrase.SceneLayer = 0;
  %phrase.setPosition(0,35);
  %phrase.setLifetime(2);
  mainScene.add(%phrase);
}

function ItFits::level5(%this)
{
  echo("Started Level 5");
  $level = 5;
  $board.setVisible(true);

  $shapes[0] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[1] = %this.makeHorizLine(getRandom(0, 4), "0 0");
  $shapes[2] = %this.makeLeftLHoriz(getRandom(0, 4), "0 0");
  $shapes[3] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[4] = %this.makeLeftLHorizInverse(getRandom(0, 4), "0 0");
  $shapes[5] = %this.makeZHoriz(getRandom(0, 4), "0 0");
  $shapes[6] = %this.makeZHoriz(getRandom(0, 4), "0 0");
  $shapes[7] = %this.makeHorizLine(getRandom(0, 4), "0 0");
  $shapes[8] = %this.makeLeftLVert(getRandom(0, 4), "0 0");
  $shapes[9] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[10] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[11] = %this.makeZVert(getRandom(0, 4), "0 0");
  $shapes[12] = %this.makeZHorizInverse(getRandom(0, 4), "0 0");
  $shapes[13] = %this.makeRightLVert(getRandom(0, 4), "0 0");
  $shapes[14] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[15] = %this.makeLeftLHorizInverse(getRandom(0, 4), "0 0");
  $shapes[16] = %this.makeVertLine(getRandom(0, 4), "0 0");
  $shapes[17] = %this.makeZHorizInverse(getRandom(0, 4), "0 0");
  $shapes[18] = %this.makeRightLHoriz(getRandom(0, 4), "0 0");
  $shapes[19] = %this.makeHorizLine(getRandom(0, 4), "0 0");
  $shapes[20] = %this.makeRightLVert(getRandom(0, 4), "0 0");
  $shapes[21] = %this.makeVertLine(getRandom(0, 4), "0 0");

  echo("Shapes count: ", $shapes.count);
  for (%i = 0; %i < 22; %i++)
  {
    $shapes[%i].setSceneLayer(5);
    mainScene.add($shapes[%i]);
    $shapes[%i].setPosition("20 0");
  }

  %phrase = new ImageFont();
  %phrase.Image = "ItFits:font";
  %phrase.FontSize = "2 3";
  %phrase.TextAlignment = "Center";
  %phrase.Text = "This is the final level";
  %phrase.SceneLayer = 0;
  %phrase.setPosition(0,35);
  %phrase.setLifetime(2);
  mainScene.add(%phrase);
}

function ItFits::nextLevel(%this)
{
  $board.setVisible(false);
  for (%i = 0; %i < 25; %i++)
  {
    $shapes[%i].safedelete();
  }
  %phrase = new ImageFont();
  %phrase.Image = "ItFits:font";
  %phrase.FontSize = "2 3";
  %phrase.TextAlignment = "Center";
  if ($level == 1)
  {
    %phrase.Text = "See...that wasn't too hard.";
  }
  else
  {
    %phrase.Text = "Good job!";
  }
  %phrase.SceneLayer = 0;
  %phrase.setLifetime(3);
  mainScene.add(%phrase);

  %totalTime = (getRealTime() - $startTime)/1000;
  %file = new FileObject();
  %file.openForAppend("Leaderboard.csv");
  %file.writeLine($level @ "," @ %totalTime);
  %file.isEOF();
  %file.close();

  %time = new ImageFont();
  %time.Image = "ItFits:font";
  %time.FontSize = "2 3";
  %time.TextAlignment = "Center";
  %time.setPosition(0, -8);
  %time.Text = ("It took you: " @ mFloatLength(%totalTime,2) @ " seconds to finish the level.");
  %time.SceneLayer = 0;
  %time.setLifetime(3);
  mainScene.add(%time);

  //%file.openForRead("Leaderboard.csv");
  //%file.readLine()

  //%bestTime = new ImageFont();
  //%bestTime.Image = "ItFits:font";
  //%bestTime.FontSize = "2 3";
  //%bestTime.TextAlignment = "Center";
  //%bestTime.setPosition(0, -16);
  //%bestTime.Text = ("The high score is : " @ %totalTime @ " seconds.");
  //%bestTime.SceneLayer = 0;
  //%bestTime.setLifetime(3);
  //mainScene.add(%bestTime);
  %file.delete();
  if ($level == 1)
  {
    ItFits.schedule(3000, "level2");
  }
  else if ($level == 2)
  {
    ItFits.schedule(3000, "level3");
  }
  else if ($level == 3)
  {
    ItFits.schedule(3000, "level4");
  }
  else if ($level == 4)
  {
    ItFits.schedule(3000, "level5");
  }
}

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

function ItFits::startingMessage( %this )
{
    %phrase = new ImageFont();
    %phrase.Image = "ItFits:font";

    // Set the font size in both axis.  This is in world-units and not typicaly font "points".
    %phrase.FontSize = "4 6";

    %phrase.TextAlignment = "Center";

    %phrase.Text = "It Fits!";
    %phrase.SceneLayer = 0;
    %phrase.setLifetime(2);
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

function ItFits::lifeLineTutorial(%this)
{
  $board.setVisible(false);
  $skip.setVisible(true);
  $delete.setVisible(true);
  $expand.setVisible(true);
  $skip.schedule(2000, "setVisible", "true");
  $delete.schedule(2000, "setVisible", "false");
  $expand.schedule(2000, "setVisible", "false");
  $skip.schedule(4000, "setVisible", "false");
  $delete.schedule(4000, "setVisible", "true");
  $delete.schedule(6000, "setVisible", "false");
  $expand.schedule(6000, "setVisible", "true");
  %message = new ImageFont();
  %message.Image = "ItFits:font";
  %message.FontSize = "1.75 3";
  %message.TextAlignment = "Center";
  %message.Text = "These are your lifelines, they can only be used once.";
  %message.setLifetime(8);
  mainScene.add(%message);
  %message.schedule(2000, "setText", "Use this to skip the level");
  %message.schedule(4000, "setText", "Use this to delete one random block");
  %message.schedule(6000, "setText", "Use this to expand the board");
}

function ItFits::pileTutorial(%this)
{
  $board.setVisible(false);
  %pile = new ImageFont();
  %pile.Image = "ItFits:font";
  // Set the font size in both axis.  This is in world-units and not typicaly font "points".
  %pile.FontSize = "1.75 3";

  %pile.TextAlignment = "Center";

  %pile.Text = "Drag shapes from this pile ->";
  %pile.SceneLayer = 0;
  %pile.setPosition(-25,0);
  %pile.setLifetime(2);
  mainScene.add(%pile);
}

function ItFits::boardTutorial(%this)
{
  $board.setVisible(true);
  for (%i = 0; %i < 13; %i++)
  {
    $shapes[%i].setVisible(false);
  }
  %line1 = new ImageFont();
  %line1.Image = "ItFits:font";
  %line1.FontSize = "1.75 3";
  %line1.TextAlignment = "Center";
  %line1.Text = "And put them on";
  %line1.SceneLayer = 0;
  %line1.setPosition(25,0);
  %line1.setLifetime(2);
  mainScene.add(%line1);
  %line2 = new ImageFont();
  %line2.Image = "ItFits:font";
  %line2.FontSize = "1.75 3";
  %line2.TextAlignment = "Center";
  %line2.Text = "<- this board";
  %line2.SceneLayer = 0;
  %line2.setPosition(15,-5);
  %line2.setLifetime(2);
  mainScene.add(%line2);
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
    %line.addSprite(%pos);
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
