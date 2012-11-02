java -jar libs\JSCover.jar -fs ^
	--no-instrument=Tests ^
	--no-instrument=jquery-1.8.2.js ^
	--no-instrument=knockout-2.2.0.debug.js ^
	--no-instrument=sammy-0.7.1.js ^
	--no-instrument=jquery-ui-1.9.0.js ^
	--no-instrument=json2.js ^
	../JamRunaway.Web/Scripts/ out/

start iexplore.exe file:///%CD%/out/jscoverage.html?Tests/Tests.html

