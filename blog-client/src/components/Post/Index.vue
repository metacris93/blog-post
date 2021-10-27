<template>
  <b-card no-body class="post overflow-hidden mt-3">
    <b-row no-gutters>
      <b-col md="6">
        <b-card-img
          height="300"
          :src="getImage"
          alt="Image"
          class="rounded-0"
        ></b-card-img>
      </b-col>
      <b-col md="6">
        <b-card-body :title="title">
          <b-card-text>
            {{ getContent }}
          </b-card-text>
          <b-card-text class="small text-muted">{{
            this.getDatePost()
          }}</b-card-text>
          <div class="d-flex justify-content-around">
            <b-button variant="info" @click="ViewPostInfo">Ver Post</b-button>
            <b-button variant="primary" @click="updatePost">Editar</b-button>
            <b-button variant="danger" @click="deletePostInformation">Eliminar</b-button>
          </div>
        </b-card-body>
      </b-col>
    </b-row>
  </b-card>
</template>
<script>
import { mapState, mapActions } from "vuex";
export default {
  data() {
    return {
      limitContent: 200,
      defaultImage: "https://picsum.photos/id/1019/200/200",
    };
  },
  props: {
    id: {
      type: String,
      default: "",
    },
    title: {
      type: String,
      default: "Default Title",
    },
    content: {
      type: String,
      default: "Default content",
    },
    createdAt: {
      type: String,
      default: "",
    },
    updatedAt: {
      type: String,
      default: "",
    },
    image: {
      type: String,
      default: "",
    },
  },
  computed: {
    ...mapState({
      blog: (state) => state.blog.blogType,
    }),
    getImage() {
      if (this.image) return this.image;
      return this.defaultImage;
    },
    getContent() {
      if (this.content.length >= this.limitContent) {
        return this.content.substring(0, this.limitContent);
      }
      return this.content;
    },
  },
  methods: {
    ...mapActions({
      deletePost: "local/deletePost",
    }),
    ViewPostInfo() {
      this.$router.push("/post/" + this.id);
    },
    getDatePost() {
      if (this.updatedAt) {
        return this.updatedAt;
      }
      return this.createdAt;
    },
    deletePostInformation() {
      this.$swal
        .fire({
          title: "¿Estás seguro de eliminar el post?",
          icon: "warning",
          showCancelButton: true,
          confirmButtonColor: "#3085d6",
          cancelButtonColor: "#d33",
          cancelButtonText: 'Cancelar',
          confirmButtonText: "Estoy seguro",
        })
        .then(async (result) => {
          if (result.isConfirmed) {
            try {
              const res = await this.deletePost(this.id);
              if (res) {
                this.$swal.fire(
                  "Eliminado",
                  "Tu post ha sido eliminado.",
                  "success"
                );
                this.$emit('reload-posts');
              }
            } catch (error) {
              this.$swal.fire("Oops", "Tu post no fue eliminado.", "error");
            }
          }
        });
    },
    updatePost() {
      console.log("actualiza post");
    },
  },
};
</script>
