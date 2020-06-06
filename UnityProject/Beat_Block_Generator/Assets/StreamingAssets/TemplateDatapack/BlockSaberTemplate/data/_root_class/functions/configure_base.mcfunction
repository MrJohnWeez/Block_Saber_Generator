#> Center the player in block then spawn platform

# Make sure player if facing the correct direction when game begins
execute as @s run function _root_class:reset_player_position
execute positioned 0 150.0 0 run fill ~-1 ~ ~-1 ~1 ~2 ~1 minecraft:air
execute positioned 0 150.0 0 run fill ~-1 ~-1 ~-1 ~1 ~-1 ~1 minecraft:black_concrete replace
execute positioned 0 150.0 0 run fill ~-1 ~ ~-1 ~1 ~ ~1 minecraft:brick_wall replace
execute positioned 0 150.0 0 run setblock ~ ~ ~ minecraft:air replace