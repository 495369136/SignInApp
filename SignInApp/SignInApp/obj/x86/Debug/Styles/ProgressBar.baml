   M S B A M L   `   `   `  ����D  @SignInApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:SignInApp.StylesSignInApp.Styles  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   MyProgressBar �� Property �� Value �� 
TargetType � RowDefinitions'   !System.Windows.VisualStateManager    VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard g� RepeatBehavior	 �� 
TargetName
 �� TargetProperty8  2System.Windows.Media.Animation.EasingPointKeyFrame $� KeyTime $� Value �� CornerRadius J� Opacity ��Name �Name � ColumnDefinitions ��Name J� RenderTransformOrigin �� ScaleY �� ScaleX �� AngleY �� AngleX �� Angle (� StretchDirection J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      (localclr-namespace:SignInApp.Styles  5      %�  &	        �� 5      "  �6   .�5   
   ��  �@�FocusVisualStyle 6-   Z� �� 5   
    �@J�SnapsToDevicePixels . 60   �� 5   
    � ��$ 32��6#   �� 5   
    � �� �#3296FA6'   �� 5	   
    � ��$	 5,0q�6$   �� 5
   
    � �� 5      �� 5      " �6"   .R�5      � ���	#00000000 5      �� 5      $
��Auto �6,    5        5      CommonStates��. 5   "     Determinate��6.     5   "   Indeterminate��. 5   &   �� $ Forever��61   .��5   *   *� $	 	Animation��6G   $'
 !(UIElement.RenderTransformOrigin)�6i   .'�5   .     $ 0:0:0��6B   $ 0.5,0.5&�6R     5   .   $ 0:0:1.5��6B   $ 1.95,0.5&�6T     5   .   $ 0:0:3��6B   $ 0.5,0.5&�6R   5   +   5   '   5   #   5      .+�5       � #��z��6   .+�5!      �� ���#000000$ 16��6:   $
 0.05Q�6L   �� 5"      ���#000000$��1q�6;   $ 16��6O   $	 0.1Q�6a   � 5#      #��z��6#   .+�5$   "   �� $ 
PART_Track��6)   � 5%   "   $ PART_Indicator��6'   }�. $
��Left=�6S    5&   &   �� 5'   *   $ width1��6;   �� 5(   *   $ width2��6;   $��0 �6K   .+�5*   &   � $ 	Animation��6+   $ 0.5,0.5&�6?   z�5+   *   V� 5,   .   .~�5-   2   �� $ -1Q�6A   $ 1Q�6M   �� 5.   2   $ 0Q�6@   $ 0Q�6K   �� 5/   2   $	 180Q�6B   T� 50   2   51   /   .+�53   *   �� ���#3296FA$ 16��6F   ._�54   .   (� $
��Left=�66   $ DownOnly=�6Q   #��z��6m    . 55   /   56   +   �� 57   *   ���#000000$��1q�6G   $ 16��6[   $	 0.1Q�6m   58   '   59   #   5:      5;      5<       5=      P� 5>       � {� .  . �5?      ��  � �� �#c5c5c56;   5@      P� 5A       �@�IsIndeterminate . . �5B      �� $ width1��6%    � ��$ 0.25* �6J   �� 5C      $ width2��6%    � ��$ 0.725* �6J   5D      5F      5H      5I      5J      