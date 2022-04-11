# Cleanup all beatsaber entities
execute run kill @e[tag=blocksaber]
execute in minecraft:the_end positioned ~ ~ ~ as @e[type=item,distance=..30] run kill @s

# Generate map blocks
execute run summon marker ~ ~ ~ {Tags:[origin,blocksaber]}
setblock ~ ~-1 ~ black_concrete
setblock ~ ~-1 ~-45 minecraft:structure_block[mode=load]{mirror:"NONE",ignoreEntities:0b,powered:0b,seed:0L,rotation:"NONE",posX:-24,posY:-21,posZ:-1,mode:"LOAD",name:"_root_class:default_environment_pt_1",showboundingbox:0b}
setblock ~ ~-2 ~-45 minecraft:redstone_block
setblock ~ ~-2 ~-44 minecraft:redstone_block
setblock ~ ~-2 ~-46 minecraft:redstone_block
fill ~ ~-2 ~-44 ~ ~-1 ~-46 minecraft:air

# Spawn map entities
execute at @e[type=marker,tag=origin,limit=1] run function _root_class:spawn_lights
execute at @e[type=marker,tag=origin,limit=1] run function _root_class:spawn_block_saber_titles

execute run function _root_class:main_menu