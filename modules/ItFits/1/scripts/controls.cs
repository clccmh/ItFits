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
      $myobj = getWord(%picked, %i);
    }
    else if ($myobj.getSceneLayer() > getWord(%picked, %i).getSceneLayer())
    {
      $myobj = getWord(%picked, %i);
      echo("touched: ", $myobj);
    }
  }
  echo("scene layer: ", $myobj.getSceneLayer());
}

function InputManager::onTouchDragged(%this, %touchID, %worldposition)
{
  if($myobj.getSceneLayer() != 20)
  {
    $myobj.setPosition((mRound(%worldposition.X/5 - 2.5)+2.5)*5, (mRound(%worldposition.Y/5 + 2.5)-2.5)*5);
  }
}

function InputManager::onTouchUp(%this, %touchID, %worldposition)
{
  $myobj = null;
}
