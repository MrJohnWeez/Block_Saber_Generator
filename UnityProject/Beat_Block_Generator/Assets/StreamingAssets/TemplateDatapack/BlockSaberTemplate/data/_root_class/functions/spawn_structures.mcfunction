# Setup main structure
setblock ~ ~-2 ~ minecraft:structure_block[mode=load]{mirror:"NONE",ignoreEntities:1b,powered:0b,seed:0L,rotation:"NONE",posX:-21,mode:"LOAD",posY:-21,sizeX:43,posZ:-48,integrity:1.0f,showair:0b,name:"_root_class:note_runway",sizeY:42,sizeZ:42,showboundingbox:0b}
setblock ~ ~-3 ~ minecraft:redstone_block
setblock ~ ~-2 ~-47 minecraft:redstone_block

# Clear all generation blocks
setblock ~ ~-2 ~-47 minecraft:air
setblock ~ ~-2 ~-48 minecraft:air
setblock ~ ~-4 ~ minecraft:air
setblock ~ ~-3 ~ minecraft:air
setblock ~ ~-2 ~ minecraft:air