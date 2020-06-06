scoreboard players set #BlockSaberGlobal PlayingSong 0
scoreboard players set #BlockSaberGlobal FinishedNotes 0
scoreboard players set #BlockSaberGlobal FinishedObsicles 0

stopsound @p[scores={PlayerPlaying=1}] music
experience set @p[scores={PlayerPlaying=1}] 0 levels
experience set @p[scores={PlayerPlaying=1}] 0 points

title @p[scores={PlayerPlaying=1}] times 0 1200 40
title @p[scores={PlayerPlaying=1}] subtitle {"text":"FAILED! (Throw Red Saber to Restart)","color":"yellow"}
title @p[scores={PlayerPlaying=1}] title ["",{"text":"Block","bold":true,"color":"red"},{"text":" Saber","bold":true,"color":"aqua"}]