if(NOT TARGET game-activity::game-activity)
add_library(game-activity::game-activity STATIC IMPORTED)
set_target_properties(game-activity::game-activity PROPERTIES
    IMPORTED_LOCATION "C:/Users/pji2918/.gradle/caches/transforms-3/812d3057888613e0ba36b827475fe757/transformed/jetified-games-activity-3.0.3/prefab/modules/game-activity/libs/android.armeabi-v7a/libgame-activity.a"
    INTERFACE_INCLUDE_DIRECTORIES "C:/Users/pji2918/.gradle/caches/transforms-3/812d3057888613e0ba36b827475fe757/transformed/jetified-games-activity-3.0.3/prefab/modules/game-activity/include"
    INTERFACE_LINK_LIBRARIES ""
)
endif()

if(NOT TARGET game-activity::game-activity_static)
add_library(game-activity::game-activity_static STATIC IMPORTED)
set_target_properties(game-activity::game-activity_static PROPERTIES
    IMPORTED_LOCATION "C:/Users/pji2918/.gradle/caches/transforms-3/812d3057888613e0ba36b827475fe757/transformed/jetified-games-activity-3.0.3/prefab/modules/game-activity_static/libs/android.armeabi-v7a/libgame-activity_static.a"
    INTERFACE_INCLUDE_DIRECTORIES "C:/Users/pji2918/.gradle/caches/transforms-3/812d3057888613e0ba36b827475fe757/transformed/jetified-games-activity-3.0.3/prefab/modules/game-activity_static/include"
    INTERFACE_LINK_LIBRARIES ""
)
endif()

