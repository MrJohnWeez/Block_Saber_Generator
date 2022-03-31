scoreboard players set #BlockSaberGlobal LevelMapSpawned 1

# cleanup old map
kill @e[type=marker,tag=origin]
kill @e[type=armor_stand,tag=blockBeat]

# generate new map
summon marker ~ ~ ~ {Tags:[origin,blockBeat]}
setblock ~ ~-1 ~ black_concrete
setblock ~ ~-1 ~-45 minecraft:structure_block[mode=load]{mirror:"NONE",ignoreEntities:0b,powered:0b,seed:0L,rotation:"NONE",posX:-24,posY:-21,posZ:-1,mode:"LOAD",name:"_root_class:default_environment_pt_1",showboundingbox:0b}
setblock ~ ~-2 ~-45 minecraft:redstone_block
setblock ~ ~-2 ~-44 minecraft:redstone_block
setblock ~ ~-2 ~-46 minecraft:redstone_block
fill ~ ~-2 ~-44 ~ ~-1 ~-46 minecraft:air
execute as @e[type=marker,tag=origin,limit=1] at @e[type=marker,tag=origin,limit=1] run function _root_class:spawn_lights
execute as @e[type=marker,tag=origin,limit=1] at @e[type=marker,tag=origin,limit=1] run function _root_class:stop
