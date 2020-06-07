#> Center the player in block then spawn platform

# Make sure player if facing the correct direction when game begins
execute as @s run function _root_class:reset_player_position
execute in minecraft:the_end positioned 0 150.0 500 run fill ~-1 ~ ~-1 ~1 ~2 ~1 minecraft:air
execute in minecraft:the_end positioned 0 150.0 500 run fill ~-1 ~-1 ~-1 ~1 ~-22 ~1 minecraft:black_concrete outline
execute in minecraft:the_end positioned 0 150.0 500 run fill ~-1 ~ ~-1 ~1 ~ ~1 minecraft:brick_wall replace
execute in minecraft:the_end positioned 0 150.0 500 run setblock ~ ~ ~ minecraft:air replace
execute in minecraft:the_end positioned 0 150.0 500 run function _root_class:spawn_structures