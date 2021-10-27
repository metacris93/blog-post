<template>
  <b-row class="justify-content-md-center">
    <b-col cols="8">
      <b-form id="formPost" @submit="onSubmit">
        <b-form-group
          id="input-title"
          label="Título:"
          label-for="title"
          label-align-xl="right"
        >
          <b-form-input
            id="title"
            v-model="form.title"
            placeholder="Título..."
            required
          ></b-form-input>
        </b-form-group>
        <b-form-group id="input-content" label="Contenido:" label-for="content">
          <b-form-textarea
            id="content"
            v-model="form.content"
            placeholder="Contenido..."
            rows="3"
            required
          ></b-form-textarea>
        </b-form-group>
        <b-form-file
          v-model="form.image"
          accept="image/jpeg, image/png, image/gif"
          @change="imageUploaded"
          placeholder="Choose a file or drop it here..."
          drop-placeholder="Drop file here..."
        ></b-form-file>
        <b-button type="submit" variant="primary">Enviar</b-button>
      </b-form>
      <!-- <h3>{{ blog }}</h3> -->
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
      blog: (state) => state.blog.blogType,
    }),
  },
  data() {
    return {
      form: {
        title: "",
        content: "",
        image: null,
        imageB64: "",
      },
    };
  },
  methods: {
    ...mapActions({
      addPost: "local/addPost",
    }),
    async onSubmit(event) {
      event.preventDefault();
      try {
        switch (this.blog) {
          case blogType.LOCAL: {
            let res = await this.addPost({
              title: this.form.title,
              content: this.form.content,
              createdAt: moment().locale("es").calendar(),
              updatedAt: "",
              image: this.form.imageB64,
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
                  title: "Post registrado",
                });
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
        console.log(error);
      }
    },
    imageUploaded(e) {
      if (e.target.files.length > 0) {
        this.form.image = e.target.files[0];
        const fileReader = new FileReader();
        fileReader.onload = (e) => {
          this.form.imageB64 = e.target.result;
        };
        fileReader.readAsDataURL(this.form.image);
      }
    },
  },
};
</script>
<style scoped></style>
