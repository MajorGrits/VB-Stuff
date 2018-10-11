/************************************************************************
			b-PAC 3.0 Component Sample (NamePlt)

		(C)Copyright Brother Industries, Ltsd. 2009
************************************************************************/

// NamePlt.h : main header file for the NAMEPLT application
//

#if !defined(AFX_NAMEPLT_H__2202B9A4_5E56_11D5_AB31_00A024CE441E__INCLUDED_)
#define AFX_NAMEPLT_H__2202B9A4_5E56_11D5_AB31_00A024CE441E__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CNamePltApp:
// See NamePlt.cpp for the implementation of this class
//

class CNamePltApp : public CWinApp
{
public:
	CNamePltApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CNamePltApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CNamePltApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_NAMEPLT_H__2202B9A4_5E56_11D5_AB31_00A024CE441E__INCLUDED_)
