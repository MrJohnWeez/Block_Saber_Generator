function _root_class:spawn_level_cleared
execute if score #BlockSaberGlobal FinishedCount matches 1 run title @p[scores={PlayerPlaying=1}] actionbar ["",{"text":"LEVEL SONGTITLE: SCORE:","color":"yellow"},{"text":" "},{"score":{"name":"#BlockSaberGlobal","objective":"PlayerScore"}}]
