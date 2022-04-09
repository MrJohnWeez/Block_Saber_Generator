#Run base class
execute as @e[type=item,nbt={Item:{tag:{display:{Name:"{\"text\":\"Red Saber\",\"italic\":false}"}}}}] run kill @s
execute as @s run function _root_class:play
execute run kill @e[tag=nodeSpawnOrigin]
execute as @s run function folder_uuid:set_spawn_orgin
scoreboard players set #BlockSaberGlobal SongID SONGID