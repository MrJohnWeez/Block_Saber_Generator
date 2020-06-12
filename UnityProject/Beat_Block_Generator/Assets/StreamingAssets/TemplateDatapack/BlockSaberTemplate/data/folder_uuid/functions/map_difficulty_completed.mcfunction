function _root_class:map_difficulty_completed
execute if score #BlockSaberGlobal FinishedCount matches 1 run title @p[scores={PlayerPlaying=1}] actionbar ["",{"text":"LEVEL SONGTITLE: SCORE:","color":"yellow"},{"text":" "},{"score":{"name":"@s","objective":"PlayerScore"}}]
