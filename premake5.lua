-- ----------------------------------------------
-- ----------------------------------------------
-- ------- Saffron 2D Project Generator ---------
-- ----------------------------------------------
-- ----------------------------------------------


-- --------------------------------------
-- Saffron Workspace
-- --------------------------------------

workspace "Saffron2D"
	architecture "x64"
	
	configurations 
	{ 
		"Debug", 
		"Release",
		"Dist"
	}

	flags
	{
		"MultiProcessorCompile"
	}

	startproject "Project"

outputDirectory = "%{cfg.buildcfg}-%{cfg.system}-%{cfg.architecture}"

-- --------------------------------------
-- Saffron
-- --------------------------------------

project "Saffron2D"
	location "%{prj.name}/Source"
	kind "SharedLib"
	language "C#"
	staticruntime "On"

	targetdir ("Bin/" .. outputDirectory .. "/%{prj.name}")
	objdir ("Bin-Int/" .. outputDirectory .. "/%{prj.name}")

	files 
	{ 
		"%{prj.name}/Source/**.cs"
	}	
	
	-- postbuildcommands 
	-- {
		-- '{COPY} "Resources/Assets" "%{cfg.targetdir}/Resources/Assets"'
	-- }
	
	nuget 
	{ 
		"SFML.Net:2.5.0"
	}

	filter "system:windows"
		systemversion "latest"

	filter "configurations:Debug"
		defines "SE_DEBUG"
		symbols "On"
			
	filter "configurations:Release"
		defines "SE_RELEASE"
		optimize "On"

	filter "configurations:Dist"
		defines "SE_DIST"
		optimize "On"
		
group ""

-- --------------------------------------
-- Project
-- --------------------------------------

project "Project"
	location "%{prj.name}/Source"
	kind "ConsoleApp"
	language "C#"
	staticruntime "On"

	targetdir ("Bin/" .. outputDirectory .. "/%{prj.name}")
	objdir ("Bin-Int/" .. outputDirectory .. "/%{prj.name}")

	files 
	{ 
		"%{prj.name}/Source/**.cs"
	}	
	
	links
	{
		"Saffron2D"
	}
	
	-- postbuildcommands 
	-- {
		-- '{COPY} "Resources/Assets" "%{cfg.targetdir}/Resources/Assets"'
	-- }
	
	nuget 
	{ 
		"SFML.Net:2.5.0"
	}

	filter "system:windows"
		systemversion "latest"

	filter "configurations:Debug"
		defines "SE_DEBUG"
		symbols "On"
			
	filter "configurations:Release"
		defines "SE_RELEASE"
		optimize "On"

	filter "configurations:Dist"
		defines "SE_DIST"
		optimize "On"
		
group ""