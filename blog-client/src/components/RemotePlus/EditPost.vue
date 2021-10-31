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
            v-model="post.Title"
            placeholder="Título..."
            required
          ></b-form-input>
        </b-form-group>
        <b-form-group id="input-content" label="Contenido:" label-for="content">
          <b-form-textarea
            id="content"
            v-model="post.Content"
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
import { mapState } from "vuex";
import { getPost, updatePost } from "@/api/post";
export default {
  computed: {
    ...mapState({
      blog: () => localStorage.blogType,
    }),
    getImage() {
      if (this.imageB64) return this.imageB64;
      return this.post.Image ?? "";
    },
  },
  data() {
    return {
      image: null,
      imageB64: "",
      post: {},
    };
  },
  async mounted() {
    const post = await getPost(this.$route.params.id);
    if (post) {
      this.post = post.data;
    }
  },
  methods: {
    async onSubmit(event) {
      event.preventDefault();
      let res = false;
      const image = this.imageB64 === "" ? this.post.Image : this.imageB64;
      res = await updatePost(this.post, {
        Id: this.post.Id,
        Title: this.post.Title,
        Content: this.post.Content,
        Updated_at: moment(Date.now()).format("YYYY-MM-DD HH:mm:ss"),
        Image: image,
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
              toast.addEventListener("mouseleave", this.$swal.resumeTimer);
            },
          })
          .fire({
            icon: "success",
            title: "Post actualizado",
          });
        this.$router.push("/" + this.blog);
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
