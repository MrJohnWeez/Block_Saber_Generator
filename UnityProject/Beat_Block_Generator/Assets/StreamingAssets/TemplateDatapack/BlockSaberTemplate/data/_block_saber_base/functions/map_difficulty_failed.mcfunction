scoreboard players set @s PlayingSong 0
stopsound @s music
scoreboard players set @s FinishedNotes 0
scoreboard players set @s FinishedObsicles 0
experience set @s 0 levels
experience set @s 0 points

title @s times 0 1200 40
title @s subtitle {"text":"FAILED! (Throw Red Saber to Restart)","color":"yellow"}
title @s title ["",{"text":"Block","bold":true,"color":"red"},{"text":" Saber","bold":true,"color":"aqua"}]