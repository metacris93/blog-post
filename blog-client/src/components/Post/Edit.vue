<template>
  <b-row class="justify-content-md-center">
    <b-col cols="8">
      <b-form id="formPost" v-if="post" @submit="onSubmit">
        <b-form-group
          id="input-title"
          label="Título:"
          label-for="title"
          label-align-xl="right"
        >
          <b-form-input
            id="title"
            v-model="post.title"
            placeholder="Título..."
            required
          ></b-form-input>
        </b-form-group>
        <b-form-group id="input-content" label="Contenido:" label-for="content">
          <b-form-textarea
            id="content"
            v-model="post.content"
            placeholder="Contenido..."
            rows="3"
            required
          ></b-form-textarea>
        </b-form-group>
        <b-form-file
          v-model="image"
          accept="image/jpeg, image/png, image/gif"
          @change="imageUploaded"
          placeholder="Choose a file or drop it here..."
          drop-placeholder="Drop file here..."
        ></b-form-file>
        <b-form-group id="input-image" label="Imagen" label-for="post-image">
          <img id="post-image" :src="getImage" alt="Image Post" />
        </b-form-group>
        <b-button class="mt-4" type="submit" variant="primary">Editar</b-button>
      </b-form>
    </b-col>
  </b-row>
</template>
<script>
import moment from "moment";
import { mapState, mapActions } from "vuex";
import blogType from "@/utils/blogType";
export default {
  computed: {
    ...mapState({
      blog: () => localStorage.blogType,
    }),
    getImage() {
      if (this.imageB64) return this.imageB64;
      return this.post.image ?? "";
    },
  },
  data() {
    return {
      image: null,
      imageB64: "",
      post: {},
    };
  },
  mounted() {
    try {
      if (localStorage.getItem("posts")) {
        const posts = JSON.parse(localStorage.getItem("posts"));
        const id = this.$route.params.id;
        const post = posts.find((item) => item.id === id);
        if (!post) {
          throw new Error("post no encontrado");
        }
        this.post = post;
      }
    } catch (error) {
      console.error(error);
    }
  },
  methods: {
    ...mapActions({
      updatePost: "local/updatePost",
    }),
    async onSubmit(event) {
      event.preventDefault();
      try {
        switch (this.blog) {
          case blogType.LOCAL: {
            const image =
              this.imageB64 === "" ? this.post.image : this.imageB64;
            let res = await this.updatePost({
              id: this.post.id,
              title: this.post.title,
              content: this.post.content,
              updatedAt: moment().locale("es").calendar(),
              image: image,
            });
            if (res) {
              this.$swal
                .mixin({
                  toast: true,
                  position: "top-end",
                  showConfirmButton: false,
                  timer: 3000,
                  timerProgressBar: true,
                  didOpen: (toast) => {
                    toast.addEventListener("mouseenter", this.$swal.stopTimer);
                    toast.addEventListener(
                      "mouseleave",
                      this.$swal.resumeTimer
                    );
                  },
                })
                .fire({
                  icon: "success",
                  title: "Post actualizado",
                });
              this.$router.push("/" + this.blog);
            }
            break;
          }
          case blogType.REMOTE:
            break;
          case blogType.REMOTE_PLUS:
            break;
          default:
            break;
        }
      } catch (error) {
        console.error(error);
      }
    },
    imageUploaded(e) {
      if (e.target.files.length > 0) {
        this.image = e.target.files[0];
        const fileReader = new FileReader();
        fileReader.onload = (e) => {
          this.imageB64 = e.target.result;
        };
        fileReader.readAsDataURL(this.image);
      }
    },
  },
};
</script>
<style scoped></style>
