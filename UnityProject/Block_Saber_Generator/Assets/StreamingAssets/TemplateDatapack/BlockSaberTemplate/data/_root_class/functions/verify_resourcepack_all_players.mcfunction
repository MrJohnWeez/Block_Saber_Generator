# Verify Resourcepack and show error if player does not have it installed
execute at @a run summon item ~ ~ ~ {Age:-32768,PickupDelay:32767,Item:{id:"minecraft:structure_void",Count:1b,tag:{HideFlags:127}}}
scoreboard players set @a TempVar1 0
execute as @a positioned as @s at @e[type=item,name="BeatSaber Void",distance=..2] run scoreboard players set @s TempVar1 1
execute as @a if score @s TempVar1 matches 0 run tellraw @s ["",{"text":"ERROR!","bold":true,"color":"#ff0003","hoverEvent":{"action":"show_text","contents":"Resource pack is not loaded within this world or is missing"}},{"text":" Missing Resource pack!\nUnable to play (some) Songs","color":"#ff0003","hoverEvent":{"action":"show_text","contents":"Resource pack is not loaded within this world or is missing"}}]
scoreboard players set @a TempVar1 0
execute as @e[type=item,name="BeatSaber Void"] run kill @s

# Tellraw for error message
# https://minecraft.tools/en/tellraw.php?tellraw=%3Cins%20data-text%3D%22Resource%20pack%20is%20not%20loaded%20within%20this%20world%20or%20is%20missing%22%20class%3D%22text%22%3E%3Cspan%20style%3D%22color%3A%23FF5555%22%3E%3Cstrong%3EERROR!%3C%2Fstrong%3E%20Missing%20Resource%20pack!%3Cbr%20%2F%3EUnable%20to%20play%20Songs%3C%2Fspan%3E%3C%2Fins%3E&selector=%40s