#> Center the player at a intersection of 4 blocks then spawn playform

# Make sure player if facing the correct direction when game begins
execute as @s run function block_saber_base:reset_player_position
execute at @s run fill ~-1 ~ ~-1 ~1 ~2 ~1 minecraft:air
execute at @s run setblock ~ ~-1 ~ minecraft:black_concrete
execute at @s run setblock ~1 ~ ~ minecraft:brick_wall replace
execute at @s run setblock ~ ~ ~-1 minecraft:brick_wall replace
execute at @s run setblock ~-1 ~ ~ minecraft:brick_wall replace
execute at @s run setblock ~ ~ ~1 minecraft:brick_wall replace