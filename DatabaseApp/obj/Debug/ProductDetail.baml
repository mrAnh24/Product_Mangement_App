   M S B A M L   `   `   `  ����F  BDatabaseApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DatabaseAppDatabaseApp  5   	       DatabaseApp.ProductDetail   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:DatabaseApp     5   	     � WindowStyle$
  None=�61    � WindowStartupLocation$ CenterOwner=�5       � 
ResizeMode$ NoResize=�6?    � Title$ ProductDetail��5   	   $	��400��6   $	��400��6,   '  !System.Windows.Shell.WindowChrome   WindowChrome 5	        5
   
      GlassFrameThickness$ 0q�6      CornerRadius$ 0��6/      CaptionHeight$ 0Q�6@   .��5      � 
������ � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      �� 5      $��60 �6   .+�5   
   � $��0��6   .+�5      �� -   5        $��
txtProduct��5      $��Placeholer Product��6*   $��20(�6D   $��Center=�5      $��Center=�65   5      � 5   
   $��1��6   .+�5      �� $��0,0,20,0q�6   .��5      �� -   5        $��txtDescription��5   0   
�������$��20,0,0,0q�6F   $��Wall of text��6X   $��18(�6l   $��WrapWithOverflow=�5      5      5      � 5    
   $��2��6   .+�5!      �� -   5        $��btnAdd��5!      $	��Add��6#   $��Center=�61   $��	0,0,140,0q�6N   	 � Cursor$
	 Hand��5"   *   �� 5#      -   5        $��	btnEscape��5#      $��Cancel��6&   $��Center=�67   $��	140,0,0,0q�6T   $
	 Hand��5$   -   5%      5&      5'      