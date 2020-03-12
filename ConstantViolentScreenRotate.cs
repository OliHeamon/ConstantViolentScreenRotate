using Microsoft.Xna.Framework;
using System.Reflection;
using Terraria;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace ConstantViolentScreenRotate
{
	public class ConstantViolentScreenRotate : Mod
	{
        private bool UpdateMatrixFirst(On.Terraria.Graphics.SpriteViewMatrix.orig_ShouldRebuild orig, SpriteViewMatrix self)
        {
            return false;
        }

        float rotation = 0;
        public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
        {
            rotation += Main.rand.NextFloat(-0.6f, 0.6f);

            var type = typeof(SpriteViewMatrix);
            var field = type.GetField("_transformationMatrix", BindingFlags.NonPublic | BindingFlags.Instance);

            Matrix rotation2 = Matrix.CreateRotationZ(rotation);
            Matrix translation = Matrix.CreateTranslation(new Vector3(Main.screenWidth / 2, Main.screenHeight / 2, 0));
            Matrix translation2 = Matrix.CreateTranslation(new Vector3(Main.screenWidth / -2, Main.screenHeight / -2, 0));

            field.SetValue(Transform, (translation2 * rotation2) * translation);
            base.ModifyTransformMatrix(ref Transform);
        }

        public override void Load()
        {
            On.Terraria.Graphics.SpriteViewMatrix.ShouldRebuild += UpdateMatrixFirst;
        }
    }
}