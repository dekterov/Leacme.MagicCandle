// Copyright (c) 2017 Leacme (http://leac.me). View LICENSE.md for more information.
using Godot;
using System;

public class Main : Spatial {

	public AudioStreamPlayer Audio { get; } = new AudioStreamPlayer();

	private void InitSound() {
		if (!Lib.Node.SoundEnabled) {
			AudioServer.SetBusMute(AudioServer.GetBusIndex("Master"), true);
		}
	}

	public override void _Notification(int what) {
		if (what is MainLoop.NotificationWmGoBackRequest) {
			GetTree().ChangeScene("res://scenes/Menu.tscn");
		}
	}

	public override void _Ready() {
		var env = GetNode<WorldEnvironment>("sky").Environment;
		env.BackgroundColor = new Color(Lib.Node.BackgroundColorHtmlCode);
		env.GlowEnabled = true;
		env.GlowIntensity = 1;
		env.GlowStrength = 1.4f;
		InitSound();
		AddChild(Audio);

		GetNode("candle").GetNode<MeshInstance>("Candle").MaterialOverride = new SpatialMaterial() {
			Roughness = 0.3f,
			AlbedoColor = new Color(Lib.Node.CandleColorHtmlCode),
			EmissionEnabled = true,
			Emission = new Color(Lib.Node.CandleColorHtmlCode)
		};

	}

	public override void _Process(float delta) {
		if (Input.GetGravity().Length() > 0.1) {
			var tg = -Input.GetGravity().Normalized();
			tg.y = 0;
			GetNode<CPUParticles>("Flame").Rotation = tg;
		}
	}

}
