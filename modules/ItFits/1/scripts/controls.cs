function InputManager::create(%this)
{
  echo("input manager created");
}

function InputManager::onTouchDown(%this, %touchID, %worldposition)
{
  %picked = mainScene.pickPoint(%worldposition);
  for (%i=0; %i<%picked.count; %i++)
  {
    if ($myobj == null)
    {
      %sprite = getWord(%picked, %i);
      if (%sprite.getSceneLayer() == 2)
      {
        if (%sprite == $delete && %sprite.frame == 1)
        {
          $shapes[getRandom(0, 13)].setPosition("80 0");
          $delete.frame = 0;
        } else if (%sprite == $skip && %sprite.frame == 1)
        {
          ItFits::nextLevel();
          //$skip.frame = 0;
        } else if (%sprite == $expand && %sprite.frame ==1)
        {
          $board.Image = "ItFits:expandedboard";
          $board.size = "50 60";
          $expand.frame = 0;
        }
      }
      if (getWord(%picked, %i).getSceneLayer() == 5)
      {
        $myobj = getWord(%picked, %i);
      }
    }
    else if ($myobj.getSceneLayer() > getWord(%picked, %i).getSceneLayer())
    {
      if (getWord(%picked, %i).getSceneLayer() == 5)
      {
        $myobj = getWord(%picked, %i);
      }
    }
  }
}

function InputManager::onTouchDragged(%this, %touchID, %worldposition)
{
  if($myobj.getSceneLayer() == 5)
  {
    //echo("Position: ", (mRound(%worldposition.X/5 - 2.5)+2.5)*5, " ", (mRound(%worldposition.Y/5 + 2.5)-2.5)*5);
    $myobj.setPosition((mRound(%worldposition.X/5 - 2.5)+2.5)*5, (mRound(%worldposition.Y/5 + 2.5)-2.5)*5);
    //$myobj.setPosition(%worldposition);
  }
}

function InputManager::onTouchUp(%this, %touchID, %worldposition)
{
  if ($myobj != null)
  {
    if (!%this.isOnBoard(%worldposition))
    {
      $notOnBoard.setVisible(true);
      $notOnBoard.schedule(750, "setVisible", false);
      $myobj.setPosition(20, 0);
    }
    else if (%this.testOverlap(%worldposition))
    {
      $overlap.setVisible(true);
      $overlap.schedule(750, "setVisible", false);
      $myobj.setPosition(20, 0);
    }
    %picked = mainScene.pickPoint(20, 0);
    if (%picked.count == 0)
    {
      ItFits.nextLevel();
    }
    $myobj = null;
  }
}

function InputManager::testOverlap(%this, %worldposition) {
  %picked = mainScene.pickPoint(%worldposition);
  for (%i=0; %i<%picked.count; %i++)
  {
    %sprite = getWord(%picked, %i);
    if (%sprite.getSceneLayer() != 20)
    {
      for (%x = 1; %x <= %sprite.getSpriteCount(); %x++)
      {
        %sprite.selectSpriteId(%x);
        %blockPosition = (%sprite.getPosition().X + %sprite.getSpriteLocalPosition().X SPC %sprite.getPosition().Y + %sprite.getSpriteLocalPosition().Y);
        %secondPicked = mainScene.pickPoint(%blockPosition);
        for (%k = 0; %k < %secondPicked.count; %k++)
        {
          %secondSprite = getWord(%secondPicked, %k);
          if (%secondSprite.getSceneLayer() != 20)
          {
            for (%y = 1; %y <= %secondSprite.getSpriteCount(); %y++)
            {
              if (%sprite != %secondSprite)
              {
                %secondSprite.selectSpriteId(%y);
                %secondBlockPosition = (%secondSprite.getPosition().X + %secondSprite.getSpriteLocalPosition().X SPC %secondSprite.getPosition().Y + %secondSprite.getSpriteLocalPosition().Y);
                echo("Block ", %i, " position: ", %blockPosition, ", Block ", %k, " position: ", %secondBlockPosition);
                if (%secondBlockPosition.X == %blockPosition.X && %secondBlockPosition.Y == %blockPosition.Y)
                {
                  echo("FAILED: Block ", %i, " position: ", %blockPosition, ", Block ", %k, " position: ", %secondBlockPosition);
                  return true;
                }
              }
            }
          }
        }
      }
    }
  }
  return false;
}

function InputManager::isOnBoard(%this, %worldposition)
{
  %count = 0;
  %picked = mainScene.pickPoint(%worldposition);
  for (%i=0; %i<%picked.count; %i++)
  {
    %sprite = getWord(%picked, %i);
    if (%sprite.getSceneLayer() != 20)
    {
      for (%x = 1; %x <= %sprite.getSpriteCount(); %x++)
      {
        %sprite.selectSpriteId(%x);
        echo("Sprite: ", %i, " X: ", %sprite.getSpriteLocalPosition().X, " Y: ", %sprite.getSpriteLocalPosition().Y);
        %blockPosition = (%sprite.getPosition().X + %sprite.getSpriteLocalPosition().X SPC %sprite.getPosition().Y + %sprite.getSpriteLocalPosition().Y);
        %blockPoint = mainScene.pickPoint(%blockPosition);
        for (%y = 0; %y < %blockPoint.count; %y++)
        {
          if (getWord(%blockPoint, %y).getSceneLayer() == 20)
          {
            %count++;
          }
        }
      }
    }
  }

  if (%picked.count > 1 && %count == (%picked.count-1)*4)
  {
    return true;
  }
  return false;
}
