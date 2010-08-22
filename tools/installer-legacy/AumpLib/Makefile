###############################################################################
# 
# aumplib Makefile
# Copyright (C) 2004 J. A. Robson, http://www.arbingersys.com
# 
# This library is free software; you can redistribute it and/or
# modify it under the terms of the GNU Lesser General Public
# License as published by the Free Software Foundation; either
# version 2.1 of the License, or (at your option) any later version.
# 
# This library is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
# Lesser General Public License for more details.
# 
# You should have received a copy of the GNU Lesser General Public
# License along with this library; if not, write to the Free Software
# Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
#
###############################################################################

CSC=csc.exe
WL=/w:0

# dummy target
dummy:
	-@echo Nothing made. Try 'nmake /n all'.


# make everything, including tests
all: tests


# tests hence

# make all tests
tests: TestForm

# compile testfrm test
# compile testfrm test
TestForm:
	$(CSC) /out:tests\TestForm.exe \
   	tests\TestForm.cs \
	Aumpel.cs \
	LameWrapper.cs \
	LameWriter.cs \
	MadlldlibWrapper.cs \
	MadnpsrvWrapper.cs \
	LibsndfileWrapper.cs \
	MP3Check.cs \
	$(WL)


# Compile MP3Check to DLL
MP3CheckLib:
	$(CSC) /target:library MP3Check.cs


# remove all compiled files
clean:
	-@echo Removing compiled files...
	-@del tests\TestForm.exe


