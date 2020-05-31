# Increase health by 1%
experience set @s 28 levels
execute as @s[scores={Multiplier=1}] run experience add @s 1 points
execute as @s[scores={Multiplier=2}] run experience add @s 2 points
execute as @s[scores={Multiplier=4}] run experience add @s 4 points
execute as @s[scores={Multiplier=8}] run experience add @s 8 points

scoreboard players set @s TempVar1 0
execute as @s[level=29] run scoreboard players set @s TempVar1 1

execute as @s[scores={TempVar1=1, Multiplier=1}] run experience set @s 2 levels
execute as @s[scores={TempVar1=1, Multiplier=2}] run experience set @s 4 levels
execute as @s[scores={TempVar1=1, Multiplier=4}] run experience set @s 8 levels

execute as @s[scores={TempVar1=1}] run experience set @s 0 points

execute as @s[scores={Multiplier=1}] run experience set @s 1 levels
execute as @s[scores={Multiplier=2}] run experience set @s 2 levels
execute as @s[scores={Multiplier=4}] run experience set @s 4 levels
execute as @s[scores={Multiplier=8}] run experience set @s 8 levels

# Add to player's total score
scoreboard players operation @s PlayerScore += @s Multiplier