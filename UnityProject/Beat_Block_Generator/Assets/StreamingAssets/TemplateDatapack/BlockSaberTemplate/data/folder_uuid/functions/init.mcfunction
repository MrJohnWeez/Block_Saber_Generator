scoreboard players set #BlockSaberGlobal TempVar3 0
execute store success score #BlockSaberGlobal TempVar3 run playsound minecraft:hotbeat2 music @a 0 150.0 500 1
stopsound @a music
execute if score #BlockSaberGlobal TempVar3 matches 0 as @a run function folder_uuid:resourcepack_error