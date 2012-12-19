//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;

namespace GPRP.GPRPComponents {

	public class Style : IComparable{


		#region Fields
		#endregion

		#region Properties
		public	int		StyleId {
			get{return _styleId;}
			set{_styleId = value ; }
		}
		public string	StyleName {
			get{return _styleName;}
			set{_styleName = value ; }
		}
		public string	StyleSheetTemplate {
			get{return _styleSheetTemplate;}
			set{_styleSheetTemplate = value ; }
		}
		public string	BodyBackgroundColor {
			get{return _bodyBackgroundColor;}
			set{_bodyBackgroundColor = value ; }
		}
		public string	BodyTextColor {
			get{return _bodyTextColor;}
			set{_bodyTextColor = value ; }
		}
		public string	LinkVisited {
			get{return _linkVisited;}
			set{_linkVisited = value ; }
		}
		public string	LinkHover {
			get{return _linkHover;}
			set{_linkHover = value ; }
		}
		public string	LinkActive {
			get{return _linkActive;}
			set{_linkActive = value ; }
		}
		public string	RowColorPrimary {
			get{return _rowColorPrimary;}
			set{_rowColorPrimary = value ; }
		}
		public string	RowColorSecondary {
			get{return _rowColorSecondary;}
			set{_rowColorSecondary = value ; }
		}
		public string	RowColorTertiary {
			get{return _rowColorTertiary;}
			set{_rowColorTertiary = value ; }
		}
		public string	RowClassPrimary {
			get{return _rowClassPrimary;}
			set{_rowClassPrimary = value ; }
		}
		public string	RowClassSecondary {
			get{return _rowClassSecondary;}
			set{_rowClassSecondary = value ; }
		}
		public string	RowClassTertiary {
			get{return _rowClassTertiary;}
			set{_rowClassTertiary = value ; }
		}
		public string	HeaderColorPrimary {
			get{return _headerColorPrimary;}
			set{_headerColorPrimary = value ; }
		}
		public string	HeaderColorSecondary {
			get{return _headerColorSecondary;}
			set{_headerColorSecondary = value ; }
		}
		public string	HeaderColorTertiary {
			get{return _headerColorTertiary;}
			set{_headerColorTertiary = value ; }
		}
		public string	HeaderStylePrimary {
			get{return _headerStylePrimary;}
			set{_headerStylePrimary = value ; }
		}
		public string	HeaderStyleSecondary {
			get{return _headerStyleSecondary;}
			set{_headerStyleSecondary = value ; }
		}
		public string	HeaderStyleTertiary {
			get{return _headerStyleTertiary;}
			set{_headerStyleTertiary = value ; }
		}
		public string	CellColorPrimary {
			get{return _cellColorPrimary;}
			set{_cellColorPrimary = value ; }
		}
		public string	CellColorSecondary {
			get{return _cellColorSecondary;}
			set{_cellColorSecondary = value ; }
		}
		public string	CellColorTertiary {
			get{return _cellColorTertiary;}
			set{_cellColorTertiary = value ; }
		}
		public string	CellClassPrimary {
			get{return _cellClassPrimary;}
			set{_cellClassPrimary = value ; }
		}
		public string	CellClassSecondary {
			get{return _cellClassSecondary;}
			set{_cellClassSecondary = value ; }
		}
		public string	CellClassTertiary {
			get{return _cellClassTertiary;}
			set{_cellClassTertiary = value ; }
		}
		public string	FontFacePrimary {
			get{return _fontFacePrimary;}
			set{_fontFacePrimary = value ; }
		}
		public string	FontFaceSecondary {
			get{return _fontFaceSecondary;}
			set{_fontFaceSecondary = value ; }
		}
		public string	FontFaceTertiary {
			get{return _fontFaceTertiary;}
			set{_fontFaceTertiary = value ; }
		}
		public short	FontSizePrimary {
			get{return _fontSizePrimary;}
			set{_fontSizePrimary = value ; }
		}
		public short	FontSizeSecondary {
			get{return _fontSizeSecondary;}
			set{_fontSizeSecondary = value ; }
		}
		public short	FontSizeTertiary {
			get{return _fontSizeTertiary;}
			set{_fontSizeTertiary = value ; }
		}
		public string	FontColorPrimary {
			get{return _fontColorPrimary;}
			set{_fontColorPrimary = value ; }
		}
		public string	FontColorSecondary {
			get{return _fontColorSecondary;}
			set{_fontColorSecondary = value ; }
		}
		public string	FontColorTertiary {
			get{return _fontColorTertiary;}
			set{_fontColorTertiary = value ; }
		}
		public string	SpanClassPrimary {
			get{return _spanClassPrimary;}
			set{_spanClassPrimary = value ; }
		}
		public string	SpanClassSecondary {
			get{return _spanClassSecondary;}
			set{_spanClassSecondary = value ; }
		}
		public string	SpanClassTertiary {
			get{return _spanClassTertiary;}
			set{_spanClassTertiary = value ; }
		}
		#endregion

		#region Events
		#endregion

		#region Public Methods

		public Style() {
		}

		#endregion

		#region Protected Methods
		#endregion

		#region Protected Data
		#endregion

		#region Private Methods
		#endregion

		#region Private Data
		private int		_styleId;
		private string	_styleName;
		private string	_styleSheetTemplate;
		private string	_bodyBackgroundColor;
		private string	_bodyTextColor;
		private string	_linkVisited;
		private string	_linkHover;
		private string	_linkActive;
		private string	_rowColorPrimary;
		private string	_rowColorSecondary;
		private string	_rowColorTertiary;
		private string	_rowClassPrimary;
		private string	_rowClassSecondary;
		private string	_rowClassTertiary;
		private string	_headerColorPrimary;
		private string	_headerColorSecondary;
		private string	_headerColorTertiary;
		private string	_headerStylePrimary;
		private string	_headerStyleSecondary;
		private string	_headerStyleTertiary;
		private string	_cellColorPrimary;
		private string	_cellColorSecondary;
		private string	_cellColorTertiary;
		private string	_cellClassPrimary;
		private string	_cellClassSecondary;
		private string	_cellClassTertiary;
		private string	_fontFacePrimary;
		private string	_fontFaceSecondary;
		private string	_fontFaceTertiary;
		private short	_fontSizePrimary;
		private short	_fontSizeSecondary;
		private short	_fontSizeTertiary;
		private string	_fontColorPrimary;
		private string	_fontColorSecondary;
		private string	_fontColorTertiary;
		private string	_spanClassPrimary;
		private string	_spanClassSecondary;
		private string	_spanClassTertiary;


		#endregion
	
		#region IComparable Members

		public int CompareTo(object obj) {
			// TODO:  Add Style.CompareTo implementation
			return 0;
		}

		#endregion
	}

}