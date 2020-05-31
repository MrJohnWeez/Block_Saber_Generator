execute as @s run function _block_saber_base:map_difficulty_completed
execute if score @s FinishedCount matches 1 run title @s times 0 100 40
execute if score @s FinishedCount matches 1 run title @s subtitle ["",{"text":"Completed SONGTITLE: Score:","color":"yellow"},{"text":" "},{"score":{"name":"@s","objective":"PlayerScore"}}]
execute if score @s FinishedCount matches 1 run title @s title ["",{"text":"Block","bold":true,"color":"red"},{"text":" Saber","bold":true,"color":"aqua"}]

