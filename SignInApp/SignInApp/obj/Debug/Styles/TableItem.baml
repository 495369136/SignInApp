   M S B A M L   `   `   `  ����D  @SignInApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:SignInApp	SignInApp  5      7 clr-namespace:SignInApp.ControlsSignInApp.Controls  5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TabItem.Static.Background  �� EndPoint �� 
StartPoint � Color � Offset   TabItem.MouseOver.Background  TabItem.Static.Border �� Color  TabItem.MouseOver.Border  TabItem.Disabled.Background  TabItem.Disabled.Border  TabItem.Selected.Border  TabItem.Selected.Background  CloseTabItem �� 
TargetType �� Property �� Value �� 
TargetType	 �Name
 J� SnapsToDevicePixels ��Name �� CornerRadius � ColumnDefinitions ��Name �� RelativeSource �� AncestorType$    SignInApp.Controls.ImageButton   Name    	ImagePath �� Triggers ^� 
Conditions �� Binding �� Value �� 
TargetName 	 ImageTableItem �� Orientation�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      !localclr-namespace:SignInApp  5      -controls clr-namespace:SignInApp.Controls  5      %�  &	        &	 �     &	 0    &	 Q    &	 r    &	 �    &	 �    &	 �    &	 �    7   7  7  7  7  &		     7   7  7  7  7  �� 5      $	  0,1&�6<   $	 0,0&�6K   .��5   
   � $ #F0F0F0��6   $	 0.0Q�6'   � 5   
   $ #E5E5E5��6   $	 1.0Q�6'   5	      �� 5      $	  0,1&�6?   $	 0,0&�6N   .��5   
   � $ #ECF4FC��6   $	 0.0Q�6'   � 5   
   $ #DCECFC��6   $	 1.0Q�6'   5      �� 5      $ #ACACAC��64   �� 5      $ #7EB4EA��67   �� 5      $ #F0F0F0��6:   �� 5      $ #D9D9D9��66   �� 5      $ #ACACAC��66   �� 5      $ #FFFFFF��6:   �� 5      " ��6!   .�5   
   ��  � �� �#1890FF6'   �� 5   
    � ��$ 60��6#   �� 5   
    � ��$
 Auto��6"   �� 5   
    � ��$	 400��6%   �� 5   
    � ��$ 18(�6%   �� 5   
    � ��$ 思源黑体 CN Medium)�6'   �� 5   
    � ��8   6'   �� 5   
    � ��8  6(   �� 5    
    � ��$ 0q�6#   �� 5!   
    � ��$ 	26,2,13,2q�6$   �� 5"   
    � ��$ Stretch=�67   �� 5#   
    � ��$ Stretch=�65   �� 5$   
    � �� 5%      �� 5&      " ��6"   .R�5'      � $	 templateRoot��6   
 . .+�5(      �� $ 
mainBorder��6!   $��0,0,1,0q�65   #��z��6O   $��0q�6y   $ 5,5,0,0��6�   � 5)       5*      �� 5+   "   $��* �63   6?   �� 5,   "   $��50 �63   6@   .+�5.      �� $ contentPresenter��6?   $��0��6/   $��Header��6Y   |�.  $��30,0,0,0q�6  ��. #
 z
 6.  ��6�   �� *HorizontalContentAlignment+ �� " ����6j  �� *VerticalContentAlignment+ �� " ��   5/      $ tabCloseButton��6C   $��1��63   $��14��6[   $ \Images\close.png��6g   
������ $��	18,0,20,0q�6  ��6�   �� *HorizontalContentAlignment+ �� " ��6  50      51       52      ^� 53       54      �� 55   "    6,   �� *IsMouseOver+ �� *Self+ true6r   �� 56   "    6,   �� *TabStripPlacement+ �� " �� Top6�   .0�58      �� $ 
mainBorder��6;   8   � ��6S   �� 59      $ 
mainBorder��6<    � ��
 �����6T   5:      ^� 5;       5<      �� 5=   "    6,   �� *	IsEnabled+ �� *Self+	 false6p   �� 5>   "    6,   �� *TabStripPlacement+ �� " �� Top6�   .0�5@      �� $ contentPresenter��68    �@J�Opacity$
 0.56Q�6V   �� 5A      $ 
mainBorder��6;   8   � ��6S   �� 5B      $ 
mainBorder��6<   8   � ��6T   5C      ^� 5D       5E      �� 5F   "    6,   �� *
IsSelected+ �� *Self+	 false6q   �� 5G   "    6,   �� *TabStripPlacement+ �� " �� Top6�   .0�5I      �� $ 
mainBorder��6@    � ��$ 0,0,1,0q�6X   �� 5J      $ 
mainBorder��6=    �@��CornerRadius$ 5,5,0,0��6U   5K      ^� 5L       5M      �� 5N   "    6,   �� *
IsSelected+ �� *Self+ true6q   �� 5O   "    6,   �� *TabStripPlacement+ �� " �� Top6�   .0�5Q      ��  �@K�ZIndex$ 1��6=   �� 5R      $ 
mainBorder��6;    � ��
 �����6S   5S      5U      5W      5X      �� 5Z      " ��6#   .�5[   
   ��  � �� �#1890FF6'   �� 5\   
    � ��$ 60��6#   �� 5]   
    � ��$
 Auto��6"   �� 5^   
    � ��$ 18(�6%   �� 5_   
    � ��$ 思源黑体 CN Medium)�6'   �� 5`   
    � ��8   6'   �� 5a   
    � ��8  6(   �� 5b   
    � ��$ Stretch=�67   �� 5c   
    � ��$ Stretch=�65   �� 5d   
    � �� 5e      �� 5f      " ��6"   .R�5g      � $	 templateRoot��6   
 . .+�5h      �� $ 
mainBorder��6!   $��0,0,1,0q�65   #��z��6O   $ 5,5,0,0��6y   �� 5i      $ 
Horizontal=�6%   $��Center=�6>   .+�5j      �� $��\Images\class.png��6$   $��21��6?   $��Uniform=�6K   $��40,0,0,0q�6]   �� 5k      $ contentPresenter��6/   $��Header��6I   |�.  $��	15,0,40,0q�6�   ��. #
 z
 6  ��6r   �� *HorizontalContentAlignment+ �� " ����6[  �� *VerticalContentAlignment+ �� " ��5l      5m       5n      ^� 5o       5p      �� 5q   "    6,   �� *IsMouseOver+ �� *Self+ true6r   �� 5r   "    6,   �� *TabStripPlacement+ �� " �� Top6�   .0�5t      �� $ 
mainBorder��6;   8   � ��6S   �� 5u      $ 
mainBorder��6<    � ��
 �����6T   5v      ^� 5w       5x      �� 5y   "    6,   �� *	IsEnabled+ �� *Self+	 false6p   �� 5z   "    6,   �� *TabStripPlacement+ �� " �� Top6�   .0�5|      �� $ contentPresenter��68    �@J�Opacity$
 0.56Q�6V   �� 5}      $ 
mainBorder��6;   8   � ��6S   �� 5~      $ 
mainBorder��6<   8   � ��6T   5      ^� 5�       5�      �� 5�   "    6,   �� *
IsSelected+ �� *Self+	 false6q   �� 5�   "    6,   �� *TabStripPlacement+ �� " �� Top6�   .0�5�      �� $ 
mainBorder��6@    � ��$ 0,0,1,0q�6X   �� 5�      $ 
mainBorder��6=    �@��CornerRadius$ 5,5,0,0��6U   5�      ^� 5�       5�      �� 5�   "    6,   �� *
IsSelected+ �� *Self+ true6q   �� 5�   "    6,   �� *TabStripPlacement+ �� " �� Top6�   .0�5�      ��  �@K�ZIndex$ 1��6=   �� 5�      $ 
mainBorder��6;    � ��
 �����6S   5�      5�      5�      5�      5�      