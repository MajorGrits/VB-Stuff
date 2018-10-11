/*  ConsoleLabel.c : b-PAC 3.0 SDK Sample for Console Program.
 *
 *  Copyright(C) Brother Industries, Ltd.   2009
 */

#include <stdio.h>
#include <process.h>

int main(int argc, char* argv[])
{
	char szBuf[256];
	char szVbsFile[] = "BcdLabel.vbs";
	char szItemName[] = "Label Writer";
	char szItemCode[] = "9200";

	/* Excutes VB-Script via wscript(WSH). */
	sprintf(szBuf, "wscript \"%s\" \"%s\" \"%s\"", szVbsFile, szItemName, szItemCode);
	system(szBuf);

	return 0;
}
