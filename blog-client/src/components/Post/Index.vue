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
            <b-button variant="danger" @click="$emit('delete-post', getID)"
              >Eliminar</b-button
            >
          </div>
        </b-card-body>
      </b-col>
    </b-row>
  </b-card>
</template>
<script>
import moment from "moment";
import { mapState } from "vuex";
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
      blog: () => localStorage.blogType,
    }),
    getPost() {
      return {
        id: this.id,
        title: this.title,
        content: this.content,
        createdAt: this.createdAt,
        updatedAt: this.updatedAt,
        image: this.image,
      };
    },
    getID() {
      return this.id;
    },
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
    ViewPostInfo() {
      if (this.blog === "local") this.$router.push("/post/" + this.id);
      else if (this.blog === "remote_plus")
        this.$router.push("/post/remote/" + this.id);
    },
    getDatePost() {
      if (this.updatedAt) {
        return moment(this.updatedAt).locale("es").calendar();
      }
      return moment(this.createdAt).locale("es").calendar();
    },
    updatePost() {
      if (this.blog === "local")
        this.$router.push("/post/" + this.id + "/edit");
      else if (this.blog === "remote_plus")
        this.$router.push("/post/remote/" + this.id + "/edit");
    },
  },
};
</script>
