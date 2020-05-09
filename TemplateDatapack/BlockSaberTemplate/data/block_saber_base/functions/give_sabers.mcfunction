replaceitem entity @s weapon.offhand minecraft:name_tag{display:{Name:"[{\"text\":\"Blue Saber\",\"italic\":false}]"},HideFlags:63,Enchantments:[{id:"minecraft:unbreaking",lvl:3}],CanDestroy:["minecraft:air"]} 64
replaceitem entity @s hotbar.0 minecraft:dead_fire_coral_block{Unbreakable:1b,HideFlags:63,CanDestroy:["minecraft:air"],CanPlaceOn:["minecraft:air"],Enchantments:[{id:"minecraft:unbreaking",lvl:3}]} 1
# replaceitem entity @s hotbar.1 minecraft:air 1
# replaceitem entity @s hotbar.2 minecraft:air 1
# replaceitem entity @s hotbar.3 minecraft:air 1
# replaceitem entity @s hotbar.4 minecraft:air 1
# replaceitem entity @s hotbar.5 minecraft:air 1
# replaceitem entity @s hotbar.6 minecraft:air 1
# replaceitem entity @s hotbar.7 minecraft:air 1
replaceitem entity @s hotbar.8 minecraft:spectral_arrow{Unbreakable:1b,HideFlags:63,CanDestroy:["minecraft:air"],CanPlaceOn:["minecraft:air"]} 1

execute at @e[type=armor_stand,tag=playerOrgin] as @e[type=item,name="Red Saber",distance=..3] at @s run function block_saber_base:play
execute at @e[type=armor_stand,tag=playerOrgin] as @e[type=item,name="Stop Game",distance=..3] as @p[scores={SongUUID=1111}] run function block_saber_base:stop