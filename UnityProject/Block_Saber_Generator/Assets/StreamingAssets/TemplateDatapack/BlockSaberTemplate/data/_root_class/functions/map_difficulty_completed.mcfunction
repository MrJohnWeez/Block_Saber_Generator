scoreboard players add #BlockSaberGlobal FinishedCount 1
execute if score #BlockSaberGlobal FinishedCount matches 1 run stopsound @a music
execute if score #BlockSaberGlobal FinishedCount matches 1 in minecraft:the_end run playsound minecraft:end_sound music @a 0 150.0 500 1
execute if score #BlockSaberGlobal FinishedCount matches 1 as @p[scores={PlayerPlaying=1}] run function blocksaber:play



# http://patorjk.com/software/taag/#p=display&f=ANSI%20Regular&t=LEVEL
# Level
# ██      ███████ ██    ██ ███████ ██      
# ██      ██      ██    ██ ██      ██      
# ██      █████   ██    ██ █████   ██      
# ██      ██       ██  ██  ██      ██      
# ███████ ███████   ████   ███████ ███████ 
execute if score #BlockSaberGlobal FinishedCount matches 1 in minecraft:the_end run summon armor_stand 0 154.5 480 {Tags:[aboutNameCreated,blockBeat],CustomName:"[{\"text\":\"██      ███████ ██    ██ ███████ ██      \",\"color\":\"#6EFFEE\"}]",CustomNameVisible:1,DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute if score #BlockSaberGlobal FinishedCount matches 1 in minecraft:the_end run summon armor_stand 0 154.2 480 {Tags:[aboutNameCreated,blockBeat],CustomName:"[{\"text\":\"██      ██      ██    ██ ██      ██      \",\"color\":\"#6EFFEE\"}]",CustomNameVisible:1,DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute if score #BlockSaberGlobal FinishedCount matches 1 in minecraft:the_end run summon armor_stand 0 153.9 480 {Tags:[aboutNameCreated,blockBeat],CustomName:"[{\"text\":\"██      █████   ██    ██ █████   ██      \",\"color\":\"#6EFFEE\"}]",CustomNameVisible:1,DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute if score #BlockSaberGlobal FinishedCount matches 1 in minecraft:the_end run summon armor_stand 0 153.6 480 {Tags:[aboutNameCreated,blockBeat],CustomName:"[{\"text\":\"██      ██       ██  ██  ██      ██      \",\"color\":\"#6EFFEE\"}]",CustomNameVisible:1,DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute if score #BlockSaberGlobal FinishedCount matches 1 in minecraft:the_end run summon armor_stand 0 153.3 480 {Tags:[aboutNameCreated,blockBeat],CustomName:"[{\"text\":\"██      ██       ██  ██  ██      ██      \",\"color\":\"#6EFFEE\"}]",CustomNameVisible:1,DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute if score #BlockSaberGlobal FinishedCount matches 1 in minecraft:the_end run summon armor_stand 0 153.0 480 {Tags:[aboutNameCreated,blockBeat],CustomName:"[{\"text\":\"███████ ███████   ████   ███████ ███████ \",\"color\":\"#6EFFEE\"}]",CustomNameVisible:1,DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}

# http://patorjk.com/software/taag/#p=display&f=ANSI%20Regular&t=CLEARED
# CLEARED
#  ██████ ██      ███████  █████  ██████  ███████ ██████  
# ██      ██      ██      ██   ██ ██   ██ ██      ██   ██ 
# ██      ██      █████   ███████ ██████  █████   ██   ██ 
# ██      ██      ██      ██   ██ ██   ██ ██      ██   ██ 
#  ██████ ███████ ███████ ██   ██ ██   ██ ███████ ██████  
execute if score #BlockSaberGlobal FinishedCount matches 1 in minecraft:the_end run summon armor_stand 0 152.0 480 {Tags:[aboutNameCreated,blockBeat],CustomName:"[{\"text\":\" ██████ ██      ███████  █████  ██████  ███████ ██████  \",\"color\":\"#6EFFEE\"}]",CustomNameVisible:1,DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute if score #BlockSaberGlobal FinishedCount matches 1 in minecraft:the_end run summon armor_stand 0 151.7 480 {Tags:[aboutNameCreated,blockBeat],CustomName:"[{\"text\":\"██      ██      ██      ██   ██ ██   ██ ██      ██   ██ \",\"color\":\"#6EFFEE\"}]",CustomNameVisible:1,DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute if score #BlockSaberGlobal FinishedCount matches 1 in minecraft:the_end run summon armor_stand 0 151.4 480 {Tags:[aboutNameCreated,blockBeat],CustomName:"[{\"text\":\"██      ██      █████   ███████ ██████  █████   ██   ██ \",\"color\":\"#6EFFEE\"}]",CustomNameVisible:1,DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute if score #BlockSaberGlobal FinishedCount matches 1 in minecraft:the_end run summon armor_stand 0 151.1 480 {Tags:[aboutNameCreated,blockBeat],CustomName:"[{\"text\":\"██      ██      ██      ██   ██ ██   ██ ██      ██   ██ \",\"color\":\"#6EFFEE\"}]",CustomNameVisible:1,DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}
execute if score #BlockSaberGlobal FinishedCount matches 1 in minecraft:the_end run summon armor_stand 0 150.8 480 {Tags:[aboutNameCreated,blockBeat],CustomName:"[{\"text\":\" ██████ ███████ ███████ ██   ██ ██   ██ ███████ ██████  \",\"color\":\"#6EFFEE\"}]",CustomNameVisible:1,DisabledSlots:4096,Invisible:1b,NoGravity:1b,Marker:1b,Invulnerable:1b,Small:1b}




# Firework commands
execute in minecraft:the_end positioned 0 150.0 500 if score #BlockSaberGlobal FinishedCount matches 1 run summon firework_rocket ~ ~3 ~-10 {LifeTime:1,FireworksItem:{id:"firework_rocket",Count:1,tag:{Fireworks:{Flight:2,Explosions:[{Type:1,Flicker:1,Trail:0,Colors:[I;11743532,6719955],FadeColors:[I;11743532,2437522]}]}}}}
execute in minecraft:the_end positioned 0 150.0 500 if score #BlockSaberGlobal FinishedCount matches 11 run summon firework_rocket ~-5 ~2 ~-9 {LifeTime:1,FireworksItem:{id:"firework_rocket",Count:1,tag:{Fireworks:{Flight:2,Explosions:[{Type:2,Flicker:1,Trail:1,Colors:[I;14602026],FadeColors:[I;15790320]}]}}}}
execute in minecraft:the_end positioned 0 150.0 500 if score #BlockSaberGlobal FinishedCount matches 21 run summon firework_rocket ~2 ~6 ~-8 {LifeTime:1,FireworksItem:{id:"firework_rocket",Count:1,tag:{Fireworks:{Flight:2,Explosions:[{Type:0,Flicker:0,Trail:1,Colors:[I;2651799,6719955],FadeColors:[I;11743532,12801229]}]}}}}
execute in minecraft:the_end positioned 0 150.0 500 if score #BlockSaberGlobal FinishedCount matches 31 run summon firework_rocket ~ ~7 ~-3 {LifeTime:1,FireworksItem:{id:"firework_rocket",Count:1,tag:{Fireworks:{Flight:2,Explosions:[{Type:1,Flicker:1,Trail:0,Colors:[I;4312372,12801229],FadeColors:[I;8073150,14602026]},{Type:4,Flicker:0,Trail:1,Colors:[I;2437522,4312372,6719955,15435844],FadeColors:[I;3887386,4408131,15790320]},{Type:2,Flicker:1,Trail:0,Colors:[I;1973019,2651799],FadeColors:[I;11250603,12801229]}]}}}}
execute in minecraft:the_end positioned 0 150.0 500 if score #BlockSaberGlobal FinishedCount matches 41 run summon firework_rocket ~4 ~6 ~-8 {LifeTime:1,FireworksItem:{id:"firework_rocket",Count:1,tag:{Fireworks:{Flight:2,Explosions:[{Type:1,Flicker:1,Trail:1,Colors:[I;4312372,12801229],FadeColors:[I;8073150,14602026]},{Type:4,Flicker:0,Trail:1,Colors:[I;2437522,4312372],FadeColors:[I;1973019,2651799]}]}}}}
execute in minecraft:the_end positioned 0 150.0 500 if score #BlockSaberGlobal FinishedCount matches 51 run summon firework_rocket ~2 ~6 ~-8 {LifeTime:1,FireworksItem:{id:"firework_rocket",Count:1,tag:{Fireworks:{Flight:2,Explosions:[{Type:0,Flicker:0,Trail:1,Colors:[I;2651799,6719955],FadeColors:[I;11743532,12801229]}]}}}}
execute in minecraft:the_end positioned 0 150.0 500 if score #BlockSaberGlobal FinishedCount matches 61 run summon firework_rocket ~-2 ~8 ~-3 {LifeTime:1,FireworksItem:{id:"firework_rocket",Count:1,tag:{Fireworks:{Flight:2,Explosions:[{Type:1,Flicker:1,Trail:0,Colors:[I;4312372,12801229],FadeColors:[I;8073150,14602026]},{Type:4,Flicker:0,Trail:1,Colors:[I;2437522,4312372,6719955,15435844],FadeColors:[I;3887386,4408131,15790320]},{Type:2,Flicker:1,Trail:0,Colors:[I;1973019,2651799],FadeColors:[I;11250603,12801229]}]}}}}
execute in minecraft:the_end positioned 0 150.0 500 if score #BlockSaberGlobal FinishedCount matches 71 run summon firework_rocket ~2 ~6 ~-3 {LifeTime:1,FireworksItem:{id:"firework_rocket",Count:1,tag:{Fireworks:{Flight:2,Explosions:[{Type:0,Flicker:0,Trail:1,Colors:[I;2651799,6719955],FadeColors:[I;11743532,12801229]}]}}}}
execute in minecraft:the_end positioned 0 150.0 500 if score #BlockSaberGlobal FinishedCount matches 81 run summon firework_rocket ~ ~3 ~-10 {LifeTime:1,FireworksItem:{id:"firework_rocket",Count:1,tag:{Fireworks:{Flight:2,Explosions:[{Type:1,Flicker:1,Trail:0,Colors:[I;11743532,6719955],FadeColors:[I;11743532,2437522]}]}}}}
execute in minecraft:the_end positioned 0 150.0 500 if score #BlockSaberGlobal FinishedCount matches 91 run summon firework_rocket ~4 ~6 ~-8 {LifeTime:1,FireworksItem:{id:"firework_rocket",Count:1,tag:{Fireworks:{Flight:2,Explosions:[{Type:1,Flicker:1,Trail:0,Colors:[I;4312372,12801229],FadeColors:[I;8073150,14602026]},{Type:4,Flicker:0,Trail:1,Colors:[I;2437522,4312372],FadeColors:[I;1973019,2651799]}]}}}}
execute in minecraft:the_end positioned 0 150.0 500 if score #BlockSaberGlobal FinishedCount matches 101 run summon firework_rocket ~-5 ~7 ~-1 {LifeTime:1,FireworksItem:{id:"firework_rocket",Count:1,tag:{Fireworks:{Flight:2,Explosions:[{Type:1,Flicker:1,Trail:1,Colors:[I;4312372,12801229],FadeColors:[I;8073150,14602026]},{Type:4,Flicker:0,Trail:1,Colors:[I;2437522,4312372,6719955,15435844],FadeColors:[I;3887386,4408131,15790320]},{Type:2,Flicker:1,Trail:0,Colors:[I;1973019,2651799],FadeColors:[I;11250603,12801229]}]}}}}
