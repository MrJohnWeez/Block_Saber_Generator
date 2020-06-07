function _root_class:stop
execute as @s run function _root_class:configure_base
scoreboard players set @s PlayerPlaying 1

# Give 3sec delay until song starts
scoreboard players set #BlockSaberGlobal TickCount -60

# Set playtime vars
scoreboard players set #BlockSaberGlobal NodeRowID 0
scoreboard players set #BlockSaberGlobal PlayingSong 1
scoreboard players set #BlockSaberGlobal HighBlockHeight 1518
scoreboard players set #BlockSaberGlobal MidBlockHeight 1515
scoreboard players set #BlockSaberGlobal LowBlockHeight 1512

effect give @p[scores={PlayerPlaying=1}] saturation 1000000 20 true
effect give @p[scores={PlayerPlaying=1}] resistance 1000000 20 true
effect give @p[scores={PlayerPlaying=1}] night_vision 1000000 1 true

gamemode adventure @p[scores={PlayerPlaying=1}]
gamerule doMobLoot false
gamerule sendCommandFeedback false

#Spawn main points
execute in minecraft:the_end run summon armor_stand 0 150.0 500 {Tags:[playerOrgin,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute in minecraft:the_end run summon armor_stand 0 150.0 500 {Tags:[fakePlayerEyes,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute in minecraft:the_end run summon armor_stand 0 150.0 500 {Tags:[nodeCursor,blockBeat],DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}

kill @e[type=armor_stand,tag=temp]