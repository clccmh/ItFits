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
      if (getWord(%picked, %i).getSceneLayer() != 20)
      {
        $myobj = getWord(%picked, %i);
      }
    }
    else if ($myobj.getSceneLayer() > getWord(%picked, %i).getSceneLayer())
    {
      if (getWord(%picked, %i).getSceneLayer() != 20)
      {
        $myobj = getWord(%picked, %i);
      }
    }
  }
}

function InputManager::onTouchDragged(%this, %touchID, %worldposition)
{
  if($myobj.getSceneLayer() != 20)
  {
    //echo("Position: ", (mRound(%worldposition.X/5 - 2.5)+2.5)*5, " ", (mRound(%worldposition.Y/5 + 2.5)-2.5)*5);
    //$myobj.setPosition((mRound(%worldposition.X/5 - 2.5)+2.5)*5, (mRound(%worldposition.Y/5 + 2.5)-2.5)*5);
    $myobj.setPosition(%worldposition);
  }
}

function InputManager::onTouchUp(%this, %touchID, %worldposition)
{
  if ($myobj != null) {
    %count = 0;
    %onBoard = true;
    %picked = mainScene.pickPoint(%worldposition);
    for (%i=0; %i<%picked.count; %i++)
    {
      %sprite = getWord(%picked, %i);
      if (%sprite.getSceneLayer() != 20)
      {
        for (%x = 1; %x <= %sprite.getSpriteCount(); %x++)
        {
          echo("sprite position x: ", %sprite.getPosition());
          %sprite.selectSpriteId(%x);
          echo("Local position X : ", %sprite.getSpriteLocalPosition());
          if (%this.isOverlapping((%worldposition.X + %sprite.getSpriteLocalPosition().X SPC %worldposition.Y + %sprite.getSpriteLocalPosition().Y)))
          {
            %count = 5;
          }
          if (!%this.isOnBoard((%worldposition.X + %sprite.getSpriteLocalPosition().X SPC %worldposition.Y + %sprite.getSpriteLocalPosition().Y)))
          {
            %onBoard = false;
          }
        }
        %count += 1;
      }
    }
    if (%count != 1 )
    {
      $overlap.setVisible(true);
      $overlap.schedule(750, "setVisible", false);
      $myobj.setPosition(20, 0);
      $myobj = null;
    }
    else if (!%onBoard) {
      $notOnBoard.setVisible(true);
      $notOnBoard.schedule(750, "setVisible", false);
      $myobj.setPosition(20, 0);
      $myobj = null;
    }
    else
    {
      $myobj = null;
    }
  }
}

function InputManager::isOverlapping(%this, %worldposition)
{
  echo("passed worldposition: ", %worldposition);
  %count = 0;
  %picked = mainScene.pickPoint(%worldposition);
  for (%i=0; %i<%picked.count; %i++)
  {
    %sprite = getWord(%picked, %i);
    if (%sprite.getSceneLayer() != 20)
    {
      %count += 1;
    }
  }
  if (%count > 1) {
    echo("overlap at: ", %worldposition);
    return true;
  } else {
    return false;
  }
}

function InputManager::isOnBoard(%this, %worldposition)
{
  %picked = mainScene.pickPoint(%worldposition);
  for (%i=0; %i<%picked.count; %i++)
  {
    %sprite = getWord(%picked, %i);
    if (%sprite.getSceneLayer() == 20)
    {
      return true;
    }
  }
  return false;
}
