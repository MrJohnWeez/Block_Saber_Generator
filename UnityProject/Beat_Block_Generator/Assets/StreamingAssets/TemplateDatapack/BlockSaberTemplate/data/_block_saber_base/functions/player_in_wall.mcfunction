# Decrease health by 2%
experience set @s 28 levels
experience add @s -2 points

execute as @s[level=27, scores={Multiplier=1, PlayingSong=1}] run function _block_saber_base:map_difficulty_failed
execute as @s[scores={Multiplier=2}] run experience set @s 1 levels
execute as @s[scores={Multiplier=4}] run experience set @s 2 levels
execute as @s[scores={Multiplier=8}] run experience set @s 4 levels

execute as @s[scores={Multiplier=1}] run experience set @s 1 levels
execute as @s[scores={Multiplier=2}] run experience set @s 2 levels
execute as @s[scores={Multiplier=4}] run experience set @s 4 levels
execute as @s[scores={Multiplier=8}] run experience set @s 8 levels